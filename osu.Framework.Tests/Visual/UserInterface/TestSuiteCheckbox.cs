// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using osu.Framework.Bindables;
using osu.Framework.Testing;

namespace osu.Framework.Tests.Visual.UserInterface
{
    public class TestSuiteCheckbox : TestSuite<TestSceneCheckbox>
    {
        /// <summary>
        /// Test safety of <see cref="IHasCurrentValue{T}"/> implementation.
        /// This is shared across all UI elements.
        /// </summary>
        [Test]
        public void TestDirectToggle()
        {
            var testBindable = TestScene.Basic.Current.GetBoundCopy();
            TestScene.AddAssert("is unchecked", assert);
            TestScene.AddAssert("bindable unchecked", () => !testBindable.Value);

            TestScene.AddStep("switch bindable directly", action);

            TestScene.AddAssert("is checked", func);
            TestScene.AddAssert("bindable checked", () => testBindable.Value);

            TestScene.AddStep("change bindable", action1);

            TestScene.AddAssert("is unchecked", assert1);
            TestScene.AddAssert("bindable unchecked", () => !testBindable.Value);
        }

        private bool assert1() => !TestScene.Basic.Current.Value;

        private void action1() => TestScene.Basic.Current = new Bindable<bool>();

        private bool func() => TestScene.Basic.Current.Value;

        private void action() => TestScene.Basic.Current.Value = true;

        private bool assert() => !TestScene.Basic.Current.Value;
    }
}
