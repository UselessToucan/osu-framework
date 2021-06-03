// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Graphics;

namespace osu.Framework.Input.InputQueues
{
    /// <summary>
    /// Holds grouped drawables for input event propagation.
    /// </summary>
    public abstract class InputQueue
    {
        /// <summary>
        /// Holds drawables that handles regular input.
        /// </summary>
        public readonly List<Drawable> Regular = new List<Drawable>();

        /// <summary>
        /// Clears underlying input queues.
        /// </summary>
        public virtual void Clear()
        {
            Regular.Clear();
        }

        /// <summary>
        /// Reverses underlying input queues.
        /// </summary>
        public virtual void Reverse()
        {
            Regular.Reverse();
        }
    }
}
