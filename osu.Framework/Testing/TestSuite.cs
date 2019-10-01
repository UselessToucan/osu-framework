// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using osu.Framework.Development;
using osu.Framework.Platform;

namespace osu.Framework.Testing
{
    [TestFixture]
    public abstract class TestSuite
    {
    }

    public abstract class TestSuite<T> : TestSuite where T : TestScene, new()
    {
        public readonly T TestScene;
        private GameHost host;
        private Task runTask;
        private ITestSceneTestRunner runner;

        protected TestSuite()
        {
            TestScene = new T();
        }

        protected virtual ITestSceneTestRunner CreateRunner() => new TestSceneTestRunner();

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

                //if (TestContext.CurrentContext.Test.MethodName != nameof(TestConstructor))
                //    TestScene.Schedule(() => TestScene.StepsContainer.Clear());
            }

            RunSetUpSteps();
        }

        protected void RunSetUpSteps()
        {
            foreach (var method in GetType().GetMethods().Where(m => m.GetCustomAttributes(typeof(SetUpStepsAttribute), false).Length > 0))
                method.Invoke(this, null);
        }

        [TearDown]
        public void RunTests()
        {
            checkForErrors();
            runner.RunTestBlocking(TestScene);
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
        /// Tests any steps and assertions in the constructor of this <see cref="Testing.TestScene"/>.
        /// This test must run before any other tests, as it relies on <see cref="StepsContainer"/> not being cleared and not having any elements.
        /// </summary>
        [Test, Order(int.MinValue)]
        public void TestConstructor()
        {
        }
    }
}
