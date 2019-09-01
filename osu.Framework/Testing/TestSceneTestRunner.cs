// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Threading;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Platform;

namespace osu.Framework.Testing
{
    public class TestSceneTestRunner : Game, ITestSceneTestRunner
    {
        private readonly TestRunner runner;

        public TestSceneTestRunner()
        {
            Add(runner = new TestRunner());
        }

        /// <summary>
        /// Blocks execution until a provided <see cref="TestScene"/> runs to completion.
        /// </summary>
        /// <param name="test">The <see cref="TestScene"/> to run.</param>
        public virtual void RunTestBlocking(TestScene test) => runner.RunTestBlocking(test);

        public class TestRunner : CompositeDrawable
        {
            private const double time_between_tests = 200;

            private Bindable<double> volume;
            private double volumeAtStartup;

            [Resolved]
            private GameHost host { get; set; }

            public TestRunner()
            {
                RelativeSizeAxes = Axes.Both;
            }

            [BackgroundDependencyLoader]
            private void load(FrameworkConfigManager config)
            {
                volume = config.GetBindable<double>(FrameworkSetting.VolumeUniversal);
                volumeAtStartup = volume.Value;
                volume.Value = 0;
            }

            protected override void Dispose(bool isDisposing)
            {
                if (volume != null) volume.Value = volumeAtStartup;
                base.Dispose(isDisposing);
            }

            protected override void LoadComplete()
            {
                base.LoadComplete();

                host.MaximumDrawHz = int.MaxValue;
                host.MaximumUpdateHz = int.MaxValue;
                host.MaximumInactiveHz = int.MaxValue;
            }

            /// <summary>
            /// Blocks execution until a provided <see cref="TestScene"/> runs to completion.
            /// </summary>
            /// <param name="test">The <see cref="TestScene"/> to run.</param>
            public void RunTestBlocking(TestScene test)
            {
                Trace.Assert(host != null, $"Ensure this runner has been loaded before calling {nameof(RunTestBlocking)}");

                bool completed = false;
                ExceptionDispatchInfo exception = null;

                void complete()
                {
                    // We want to remove the TestScene from the hierarchy on completion as under nUnit, it may have operations run on it from a different thread.
                    // This is because nUnit will reuse the same class multiple times, running a different [Test] method each time, while the GameHost
                    // is run from its own asynchronous thread.
                    RemoveInternal(test);
                    completed = true;
                }

                Schedule(() =>
                {
                    AddInternal(test);

                    Console.WriteLine($@"{(int)Time.Current}: Running {test} visual test cases...");

                    // Nunit will run the tests in the TestScene with the same TestScene instance so the TestScene
                    // needs to be removed before the host is exited, otherwise it will end up disposed

                    test.Steps.RunAll(() =>
                    {
                        Scheduler.AddDelayed(complete, time_between_tests);
                    }, e =>
                    {
                        if (e is DependencyInjectionException die)
                            exception = die.DispatchInfo;
                        else
                            exception = ExceptionDispatchInfo.Capture(e);
                        complete();
                    });
                });

                while (!completed && host.ExecutionState == ExecutionState.Running)
                    Thread.Sleep(10);

                exception?.Throw();
            }
        }
    }

    public interface ITestSceneTestRunner
    {
        /// <summary>
        /// Blocks execution until a provided <see cref="TestScene"/> runs to completion.
        /// </summary>
        /// <param name="test">The <see cref="TestScene"/> to run.</param>
        void RunTestBlocking(TestScene test);
    }
}
