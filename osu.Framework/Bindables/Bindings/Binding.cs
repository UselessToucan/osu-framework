// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace osu.Framework.Bindables.Bindings
{
    public abstract class Binding
    {
        public readonly WeakReference<IBindable> Source;
        public readonly WeakReference<IBindable> Target;

        protected Binding(IBindable source, IBindable target)
        {
            Source = new WeakReference<IBindable>(source);
            Target = new WeakReference<IBindable>(target);
        }
    }
}
