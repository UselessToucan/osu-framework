// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Framework.Testing;
using osuTK.Input;

namespace osu.Framework.Tests.Visual.Input
{
    // TODO Merge with TestSceneKeyBindingContainer?
    [HeadlessTest]
    public class TestSceneKeyBindingPriority : ManualInputManagerTestScene
    {
        [Test]
        public void Test()
        {
            TestKeyBindingReceptor keyBindingReceptor = null;
            TestStandardInputReceptor standardInputReceptor = null;

            AddStep("Add container", () =>
            {
                Clear();

                Child = new TestKeyBindingContainer
                {
                    Children = new Drawable[]
                    {
                        keyBindingReceptor = new TestKeyBindingReceptor(),
                        standardInputReceptor = new TestStandardInputReceptor()
                    }
                };
            });

            AddStep("Press and release A", () => InputManager.Key(Key.A));

            AddAssert("Keybinding triggered", () => keyBindingReceptor.Pressed);
            AddAssert("Keybinding released", () => keyBindingReceptor.Released);

            AddAssert("Regular A key was not pressed", () => !standardInputReceptor.Pressed);
        }

        private class TestKeyBindingContainer : KeyBindingContainer<TestAction>
        {
            public override IEnumerable<IKeyBinding> DefaultKeyBindings => new IKeyBinding[]
            {
                new KeyBinding(InputKey.A, TestAction.ActionA),
            };
        }

        private enum TestAction
        {
            ActionA,
        }

        private class TestKeyBindingReceptor : Container, IKeyBindingHandler<TestAction>
        {
            public bool Pressed { get; private set; }
            public bool Released { get; private set; }

            public bool OnPressed(TestAction action)
            {
                Pressed = true;
                return true;
            }

            public void OnReleased(TestAction action)
            {
                Released = true;
            }
        }

        private class TestStandardInputReceptor : Drawable
        {
            public bool Pressed { get; private set; }

            protected override bool OnKeyDown(KeyDownEvent e)
            {
                switch (e.Key)
                {
                    case Key.A:
                        Pressed = true;
                        break;
                }

                return true;
            }
        }
    }
}
