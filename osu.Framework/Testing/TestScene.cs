﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using osu.Framework.Development;
using osu.Framework.Extensions.TypeExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Platform;
using osu.Framework.Testing.Drawables;
using osu.Framework.Testing.Drawables.Steps;
using osuTK;
using osuTK.Graphics;

namespace osu.Framework.Testing
{
    [TestFixture]
    public abstract class TestScene : Container, IDynamicallyCompile
    {
        public readonly StepsContainer Steps;
        private readonly Container content;

        protected override Container<Drawable> Content => content;

        protected virtual ITestSceneTestRunner CreateRunner() => new TestSceneTestRunner();

        private GameHost host;
        private Task runTask;
        private ITestSceneTestRunner runner;

        [OneTimeSetUp]
        public void SetupGameHost()
        {
            host = new HeadlessGameHost($"{GetType().Name}-{Guid.NewGuid()}", realtime: false);
            runner = CreateRunner();

            if (!(runner is Game game))
                throw new InvalidCastException($"The test runner must be a {nameof(Game)}.");

            runTask = Task.Factory.StartNew(() => host.Run(game), TaskCreationOptions.LongRunning);

            while (!game.IsLoaded)
            {
                checkForErrors();
                Thread.Sleep(10);
            }
        }

        protected internal override void AddInternal(Drawable drawable) =>
            throw new InvalidOperationException($"Modifying {nameof(InternalChildren)} will cause critical failure. Use {nameof(Add)} instead.");

        protected internal override void ClearInternal(bool disposeChildren = true) =>
            throw new InvalidOperationException($"Modifying {nameof(InternalChildren)} will cause critical failure. Use {nameof(Clear)} instead.");

        protected internal override bool RemoveInternal(Drawable drawable) =>
            throw new InvalidOperationException($"Modifying {nameof(InternalChildren)} will cause critical failure. Use {nameof(Remove)} instead.");

        [OneTimeTearDown]
        public void DestroyGameHost()
        {
            host.Exit();
            runTask.Wait();
            host.Dispose();

            try
            {
                // clean up after each run
                host.Storage.DeleteDirectory(string.Empty);
            }
            catch
            {
            }
        }

        [SetUp]
        public void SetUpTestForNUnit()
        {
            if (DebugUtils.IsNUnitRunning)
            {
                // Since the host is created in OneTimeSetUp, all game threads will have the fixture's execution context
                // This is undesirable since each test is run using those same threads, so we must make sure the execution context
                // for the game threads refers to the current _test_ execution context for each test
                var executionContext = TestExecutionContext.CurrentContext;

                foreach (var thread in host.Threads)
                {
                    thread.Scheduler.Add(() =>
                    {
                        TestExecutionContext.CurrentContext.CurrentResult = executionContext.CurrentResult;
                        TestExecutionContext.CurrentContext.CurrentTest = executionContext.CurrentTest;
                        TestExecutionContext.CurrentContext.CurrentCulture = executionContext.CurrentCulture;
                        TestExecutionContext.CurrentContext.CurrentPrincipal = executionContext.CurrentPrincipal;
                        TestExecutionContext.CurrentContext.CurrentRepeatCount = executionContext.CurrentRepeatCount;
                        TestExecutionContext.CurrentContext.CurrentUICulture = executionContext.CurrentUICulture;
                    });
                }

                if (TestContext.CurrentContext.Test.MethodName != nameof(TestConstructor))
                    schedule(() => Steps.Clear());
            }

            RunSetUpSteps();
        }

        [TearDown]
        public void RunTests()
        {
            checkForErrors();
            runner.RunTestBlocking(this);
            checkForErrors();
        }

        private void checkForErrors()
        {
            if (host.ExecutionState == ExecutionState.Stopping)
                runTask.Wait();

            if (runTask.Exception != null)
                throw runTask.Exception;
        }

        /// <summary>
        /// Tests any steps and assertions in the constructor of this <see cref="TestScene"/>.
        /// This test must run before any other tests, as it relies on <see cref="Steps"/> not being cleared and not having any elements.
        /// </summary>
        [Test, Order(int.MinValue)]
        public void TestConstructor()
        {
        }

        protected TestScene()
        {
            Name = RemovePrefix(GetType().ReadableName());

            RelativeSizeAxes = Axes.Both;
            Masking = true;

            base.AddInternal(new Container
            {
                RelativeSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    new Box
                    {
                        Colour = new Color4(25, 25, 25, 255),
                        RelativeSizeAxes = Axes.Y,
                        Width = steps_width,
                    },
                    new BasicScrollContainer
                    {
                        Width = steps_width,
                        Depth = float.MinValue,
                        RelativeSizeAxes = Axes.Y,
                        Child = Steps = new StepsContainer
                        {
                            Direction = FillDirection.Vertical,
                            Spacing = new Vector2(3),
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                            Padding = new MarginPadding(10),
                            Child = new SpriteText
                            {
                                Font = FrameworkFont.Condensed.With(size: 16),
                                Text = Name,
                                Margin = new MarginPadding { Bottom = 5 },
                            }
                        },
                    },
                    new Container
                    {
                        Masking = true,
                        Padding = new MarginPadding
                        {
                            Left = steps_width + padding,
                            Right = padding,
                            Top = padding,
                            Bottom = padding,
                        },
                        RelativeSizeAxes = Axes.Both,
                        Child = content = new DrawFrameRecordingContainer
                        {
                            Masking = true,
                            RelativeSizeAxes = Axes.Both
                        }
                    },
                }
            });
        }

        private const float steps_width = 180;
        private const float padding = 0;

        [Obsolete("Steps were extracted to a separate StepsContainer class")]
        public void AddStep(StepButton step) => Steps.AddStep(step);

        [Obsolete("Steps were extracted to a separate StepsContainer class")]
        public StepButton AddStep(string description, Action action) => Steps.AddStep(description, action);

        [Obsolete("Steps were extracted to a separate StepsContainer class")]
        public LabelStep AddLabel(string description) => Steps.AddLabel(description);

        [Obsolete("Steps were extracted to a separate StepsContainer class")]
        protected void AddRepeatStep(string description, Action action, int invocationCount) =>
            Steps.AddRepeatStep(description, action, invocationCount);

        [Obsolete("Steps were extracted to a separate StepsContainer class")]
        protected void AddToggleStep(string description, Action<bool> action) =>
            Steps.AddToggleStep(description, action);

        [Obsolete("Parameter order didn't match other methods – switch order to fix")] // can be removed 20190919
        protected void AddUntilStep(Func<bool> waitUntilTrueDelegate, string description = null)
            => AddUntilStep(description, waitUntilTrueDelegate);

        [Obsolete("Steps were extracted to a separate StepsContainer class")]
        protected void AddUntilStep(string description, Func<bool> waitUntilTrueDelegate) => Steps.AddUntilStep(description, waitUntilTrueDelegate);

        [Obsolete("Parameter order didn't match other methods – switch order to fix")] // can be removed 20190919
        protected void AddWaitStep(int waitCount, string description = null)
            => AddWaitStep(description, waitCount);

        [Obsolete("Steps were extracted to a separate StepsContainer class")]
        protected void AddWaitStep(string description, int waitCount) => Steps.AddWaitStep(description, waitCount);

        [Obsolete("Steps were extracted to a separate StepsContainer class")]
        protected void AddSliderStep<T>(string description, T min, T max, T start, Action<T> valueChanged) where T : struct, IComparable, IConvertible =>
            Steps.AddSliderStep(description, min, max, start, valueChanged);

        [Obsolete("Steps were extracted to a separate StepsContainer class")]
        protected void AddAssert(string description, Func<bool> assert, string extendedDescription = null) => Steps.AddAssert(description, assert, extendedDescription);

        // should run inline where possible. this is to fix RunAllSteps potentially finding no steps if the steps are added in LoadComplete (else they get forcefully scheduled too late)
        private void schedule(Action action) => Scheduler.Add(action, false);

        public virtual IReadOnlyList<Type> RequiredTypes => new Type[] { };

        internal void RunSetUpSteps()
        {
            foreach (var method in GetType().GetMethods().Where(m => m.GetCustomAttributes(typeof(SetUpStepsAttribute), false).Length > 0))
                method.Invoke(this, null);
        }

        /// <summary>
        /// Remove the "TestScene" prefix from a name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string RemovePrefix(string name)
        {
            return name.Replace("TestCase", string.Empty) // TestScene used to be called TestCase. This handles consumer projects which haven't updated their naming for the near future.
                       .Replace(nameof(TestScene), string.Empty);
        }
    }
}
