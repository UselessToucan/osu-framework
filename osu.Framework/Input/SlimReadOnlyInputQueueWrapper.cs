// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Extensions.ListExtensions;
using osu.Framework.Graphics;
using osu.Framework.Lists;

namespace osu.Framework.Input
{
    /// <inheritdoc />
    public class SlimReadOnlyInputQueueWrapper : InputQueueBase<SlimReadOnlyListWrapper<Drawable>>
    {
        public SlimReadOnlyInputQueueWrapper(InputQueue inputQueue)
        {
            Regular = inputQueue.Regular.AsSlimReadOnly();
            Keybingings = inputQueue.Keybingings.AsSlimReadOnly();
        }
    }
}
