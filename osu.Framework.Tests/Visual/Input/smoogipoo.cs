// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using NUnit.Framework;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Bindings;
using osu.Framework.Testing;
using osuTK.Input;

namespace osu.Framework.Tests.Visual.Input
{
    public class smoogipoo : ManualInputManagerTestScene
    {
        [Test]
        public void TestReleaseDoesNotTriggerWithoutPress()
        {
            TestInputReceptor shownReceptor = null;
            TestInputReceptor hiddenReceptor = null;

            AddStep("set children", () =>
            {
                Child = new TestKeyBindingContainer
                {
                    Children = new[]
                    {
                        shownReceptor = new TestInputReceptor
                        {
                            Name = "Shown",
                        },
                        hiddenReceptor = new TestInputReceptor
                        {
                            Name = "Hidden",
                            Alpha = 0,
                        }
                    }
                };
            });

            AddStep("click-hold shown receptor", () =>
            {
                InputManager.MoveMouseTo(shownReceptor);
                InputManager.PressButton(MouseButton.Left);
            });
            AddStep("hide shown receptor", () => shownReceptor.Hide());
            AddStep("show hidden receptor", () => hiddenReceptor.Show());
            AddStep("release button", () => InputManager.ReleaseButton(MouseButton.Left));

            // Both pass
            AddAssert("shown pressed", () => shownReceptor.Pressed);
            AddAssert("hidden not pressed", () => !hiddenReceptor.Pressed);

            // Both fail
            AddAssert("shown released", () => shownReceptor.Released);
            AddAssert("hidden not released", () => !hiddenReceptor.Released);
        }

        private class TestInputReceptor : CompositeDrawable, IKeyBindingHandler<TestAction>
        {
            public bool Pressed;
            public bool Released;

            public bool OnPressed(TestAction action)
            {
                Pressed = true;
                return true;
            }

            public bool OnReleased(TestAction action)
            {
                Released = true;
                return true;
            }
        }

        private enum TestAction
        {
            Action1
        }

        private class TestKeyBindingContainer : KeyBindingContainer<TestAction>
        {
            public override IEnumerable<KeyBinding> DefaultKeyBindings => new[]
            {
                new KeyBinding(InputKey.MouseLeft, TestAction.Action1)
            };
        }
    }
}
