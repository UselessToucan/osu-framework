﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using osu.Framework.Input;

namespace osu.Framework.Graphics
{
    public interface IHandleOnDragStart
    {
        /// <summary>
        /// Triggered whenever this Drawable is initially dragged by a held mouse click
        /// and subsequent movement.
        /// </summary>
        /// <param name="state">The state after the mouse was moved.</param>
        /// <returns>True if this Drawable accepts being dragged. If so, then future
        /// <see cref="OnDrag(InputState)"/> and <see cref="OnDragEnd(InputState)"/>
        /// events will be received. Otherwise, the event is propagated up the scene
        /// graph to the next eligible Drawable.</returns>
        bool OnDragStart(InputState state);
    }
}
