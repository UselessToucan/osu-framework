// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace osu.Framework.Bindables.Bindings
{
    public abstract class Binding<T>
    {
        protected readonly WeakReference<Bindable<T>> Source;
        protected readonly WeakReference<Bindable<T>> Target;

        protected Binding(WeakReference<Bindable<T>> source, WeakReference<Bindable<T>> target)
        {
            Source = source;
            Target = target;
        }

        public abstract void PropagateValueChange(Bindable<T> source);
    }
}
