// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Input.Bindings;

namespace osu.Framework.Input
{
    /// <summary>
    /// Holds grouped drawables for input event propagation.
    /// </summary>
    public class InputQueue
    {
        public readonly Func<Drawable> GetFocusedDrawable;

        /// <summary>
        /// Holds drawables that handles regular input.
        /// </summary>
        public readonly List<Drawable> Regular = new List<Drawable>();

        /// <summary>
        /// Holds drawables that handles key binding input.
        /// </summary>
        public readonly List<KeyBindingContainer> KeyBingingContainers = new List<KeyBindingContainer>();

        public InputQueue(Func<Drawable> getFocusedDrawable = null)
        {
            GetFocusedDrawable = getFocusedDrawable;
        }

        /// <summary>
        /// Clears underlying input queues.
        /// </summary>
        public void Clear()
        {
            Regular.Clear();
            KeyBingingContainers.Clear();
        }

        /// <summary>
        /// Reverses underlying input queues.
        /// </summary>
        public void Reverse()
        {
            Regular.Reverse();
            KeyBingingContainers.Reverse();
        }
    }
}
