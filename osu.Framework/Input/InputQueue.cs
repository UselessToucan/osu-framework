// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Input.Bindings;

namespace osu.Framework.Input
{
    public class InputQueue : ReadOnlyInputQueue
    {
        public List<Drawable> RegularList => RegularInner;
        public List<Drawable> PrioritisedList => PrioritisedInner;

        public InputQueue(Func<Drawable> getFocusedDrawable = null)
            : base(getFocusedDrawable)
        {
        }

        public void Add(Drawable drawable, bool prioritised = false)
        {
            if (prioritised || drawable is KeyBindingContainer)
                PrioritisedList.Add(drawable);
            else
                RegularList.Add(drawable);
        }

        public void Remove(Drawable drawable, bool prioritised = false)
        {
            if (prioritised || drawable is KeyBindingContainer)
                PrioritisedList.Remove(drawable);
            else
                RegularList.Remove(drawable);
        }

        /// <summary>
        /// Clears underlying input queues.
        /// </summary>
        public void Clear()
        {
            RegularList.Clear();
            PrioritisedList.Clear();
        }

        /// <summary>
        /// Reverses underlying input queues.
        /// </summary>
        public void Reverse()
        {
            RegularList.Reverse();
            PrioritisedList.Reverse();
        }

        public void RemoveAll(Predicate<Drawable> func)
        {
            RegularList.RemoveAll(func);
        }
    }
}
