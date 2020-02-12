// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace osu.Framework.Bindables.Bindings
{
    public abstract class Binding<T>
    {
        protected readonly WeakReference<Bindable<T>> Source;
        protected readonly WeakReference<Bindable<T>> Target;

        protected Binding(Bindable<T> source, Bindable<T> target)
        {
            Source = new WeakReference<Bindable<T>>(source);
            Target = new WeakReference<Bindable<T>>(target);
        }

        public abstract void PropagateValueChange(T previousValue, T value, bool bypassChecks, Bindable<T> source);

        public abstract void PropagateDefaultChange(T previousValue, T value, bool bypassChecks, Bindable<T> source);

        public abstract void PropagateDisabledChange(Bindable<T> source, bool propagateToBindings, bool bypassChecks);
    }
}
