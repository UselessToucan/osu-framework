// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using osu.Framework.Platform;

namespace osu.Framework.Testing
{
    [TestFixture]
    public abstract class TestSuite<T> where T : TestScene, new()
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
        /// Tests any steps and assertions in the constructor of this <see cref="TestScene"/>.
        /// This test must run before any other tests, as it relies on <see cref="TestScene.StepsContainer"/> not being cleared and not having any elements.
        /// </summary>
        [Test, Order(int.MinValue)]
        public void TestConstructor()
        {
        }
    }
}
