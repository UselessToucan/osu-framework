// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Input.Bindings;

namespace osu.Framework.Input.InputQueues
{
    public class NonPositionalInputQueue : InputQueue
    {
        /// <summary>
        /// Holds drawables that handles key binding input.
        /// </summary>
        public readonly List<KeyBindingContainer> KeyBingingContainers = new List<KeyBindingContainer>();

        public override void Clear()
        {
            base.Clear();
            KeyBingingContainers.Clear();
        }

        public override void Reverse()
        {
            base.Reverse();
            KeyBingingContainers.Reverse();
        }
    }
}
