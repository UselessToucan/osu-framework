// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Graphics;

namespace osu.Framework.Input
{
    /// <summary>
    /// Holds grouped drawables for input event propagation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InputQueueBase<T> where T : IList<Drawable>
    {
        /// <summary>
        /// Holds drawables that handles regular input.
        /// </summary>
        public T Regular { get; protected set; }

        /// <summary>
        /// Holds drawables that handles key bindings.
        /// </summary>
        public T Keybingings { get; protected set; }
    }
}
