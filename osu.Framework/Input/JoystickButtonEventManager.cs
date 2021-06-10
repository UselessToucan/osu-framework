// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Input.Events;
using osu.Framework.Input.States;

namespace osu.Framework.Input
{
    public class JoystickButtonEventManager : ButtonEventManager<JoystickButton>
    {
        public JoystickButtonEventManager(JoystickButton button)
            : base(button)
        {
        }

        protected override Drawable HandleButtonDown(InputState state, InputQueue targets)
        {
            var joystickPressEvent = new JoystickPressEvent(state, Button);

            Drawable result = null;
            if (targets.GetFocusedDrawable() != null)
                result = PropagateButtonEvent(new[] { targets.GetFocusedDrawable() }, joystickPressEvent);

            return result
                   ?? PropagateButtonEvent(targets.KeyBingingContainers, joystickPressEvent)
                   ?? PropagateButtonEvent(targets.Regular, joystickPressEvent);
        }

        protected override void HandleButtonUp(InputState state, List<Drawable> targets)
        {
            if (targets == null)
                return;

            PropagateButtonEvent(targets, new JoystickReleaseEvent(state, Button));
        }
    }
}
