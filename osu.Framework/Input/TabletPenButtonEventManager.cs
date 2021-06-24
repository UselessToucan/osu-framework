// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Framework.Input.States;

namespace osu.Framework.Input
{
    public class TabletPenButtonEventManager : ButtonEventManager<TabletPenButton>
    {
        public TabletPenButtonEventManager(TabletPenButton button)
            : base(button)
        {
        }

        protected override Drawable HandleButtonDown(InputState state, InputQueue targets)
        {
            var tabletPenButtonPressEvent = new TabletPenButtonPressEvent(state, Button);

            return PropagateButtonEvent(targets.GetFocusedDrawable(), tabletPenButtonPressEvent)
                   ?? PropagateButtonEvent(targets.KeyBingingContainers, tabletPenButtonPressEvent)
                   ?? PropagateButtonEvent(targets.Regular.Where(drawable => !(drawable is KeyBindingContainer) && drawable != targets.GetFocusedDrawable()).ToList(), tabletPenButtonPressEvent);
        }

        protected override void HandleButtonUp(InputState state, List<Drawable> targets)
        {
            if (targets == null)
                return;

            PropagateButtonEvent(targets, new TabletPenButtonReleaseEvent(state, Button));
        }
    }
}
