﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Testing;

namespace osu.Framework.Tests.Visual.Input
{
    public class TestSuiteHandleInput : ManualInputManagerTestSuite<TestSceneHandleInput>
    {
        public TestSuiteHandleInput()
        {
            AddStep($"enable {TestScene.TestNotHandleInput}", () => { InputManager.MoveMouseTo(TestScene.TestNotHandleInput); });
            AddAssert($"check {nameof(TestScene.TestNotHandleInput)}", () => !TestScene.TestNotHandleInput.IsHovered && !TestScene.TestNotHandleInput.HasFocus);

            AddStep($"enable {nameof(TestScene.TestHandlePositionalInput)}", () =>
            {
                TestScene.TestHandlePositionalInput.Enabled = true;
                InputManager.MoveMouseTo(TestScene.TestHandlePositionalInput);
            });
            AddAssert($"check {nameof(TestScene.TestHandlePositionalInput)}", () => TestScene.TestHandlePositionalInput.IsHovered && TestScene.TestHandlePositionalInput.HasFocus);

            AddStep($"enable {nameof(TestScene.TestHandleNonPositionalInput)}", () =>
            {
                TestScene.TestHandleNonPositionalInput.Enabled = true;
                InputManager.MoveMouseTo(TestScene.TestHandleNonPositionalInput);
                InputManager.TriggerFocusContention(null);
            });
            AddAssert($"check {nameof(TestScene.TestHandleNonPositionalInput)}", () => !TestScene.TestHandleNonPositionalInput.IsHovered && TestScene.TestHandleNonPositionalInput.HasFocus);

            AddStep("move mouse", () => InputManager.MoveMouseTo(TestScene.TestHandlePositionalInput));
            AddStep("disable all", () =>
            {
                TestScene.TestHandlePositionalInput.Enabled = false;
                TestScene.TestHandleNonPositionalInput.Enabled = false;
            });
            AddAssert($"check {nameof(TestScene.TestHandlePositionalInput)}", () => !TestScene.TestHandlePositionalInput.IsHovered);
            // focus is not released when AcceptsFocus become false while focused
            //AddAssert($"check {nameof(handleNonPositionalInput)}", () => !handleNonPositionalInput.HasFocus);
        }
    }
}
