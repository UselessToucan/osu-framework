// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Graphics;

namespace osu.Framework.Input
{
    /// <inheritdoc />
    public class InputQueue : InputQueueBase<List<Drawable>>
    {
        public InputQueue()
        {
            Regular = new List<Drawable>();
            Keybingings = new List<Drawable>();
        }

        /// <summary>
        /// Clears underlying input queues.
        /// </summary>
        public void Clear()
        {
            Regular.Clear();
            Keybingings.Clear();
        }

        /// <summary>
        /// Reverses underlying input queues.
        /// </summary>
        public void Reverse()
        {
            Regular.Reverse();
            Keybingings.Reverse();
        }
    }
}
