// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using osu.Framework.Bindables;
using osu.Framework.Testing;

namespace osu.Framework.Tests.Visual.UserInterface.Checkbox
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
            TestScene.AddAssert("is unchecked", () => !TestScene.Basic.Current.Value);
            TestScene.AddAssert("bindable unchecked", () => !testBindable.Value);

            TestScene.AddStep("switch bindable directly", () => TestScene.Basic.Current.Value = true);

            TestScene.AddAssert("is checked", () => TestScene.Basic.Current.Value);
            TestScene.AddAssert("bindable checked", () => testBindable.Value);

            TestScene.AddStep("change bindable", () => TestScene.Basic.Current = new Bindable<bool>());

            TestScene.AddAssert("is unchecked", () => !TestScene.Basic.Current.Value);
            TestScene.AddAssert("bindable unchecked", () => !testBindable.Value);
        }
    }
}
