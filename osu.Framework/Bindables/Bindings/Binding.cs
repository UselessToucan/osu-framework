// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace osu.Framework.Bindables.Bindings
{
    public abstract class Binding<T>
    {
        public readonly WeakReference<Bindable<T>> Source;
        public readonly WeakReference<Bindable<T>> Target;

        protected Binding(Bindable<T> source, Bindable<T> target)
        {
            Source = new WeakReference<Bindable<T>>(source);
            Target = new WeakReference<Bindable<T>>(target);
        }

        public abstract void Unbind();
    }
}
