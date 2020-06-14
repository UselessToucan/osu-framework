// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Framework.Input.States;
using osuTK;
using osuTK.Input;

namespace osu.Framework.Input.Events
{
    /// <summary>
    /// An event represeting the start of a mouse drag.
    /// </summary>
    public class DragStartEvent : MouseButtonEvent
    {
        public readonly Direction Direction;

        public DragStartEvent(InputState state, MouseButton button, Direction direction, Vector2? screenSpaceMouseDownPosition = null)
            : base(state, button, screenSpaceMouseDownPosition)
        {
            Direction = direction;
        }
    }
}
