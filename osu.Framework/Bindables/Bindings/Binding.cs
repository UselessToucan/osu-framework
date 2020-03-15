﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace osu.Framework.Bindables.Bindings
{
    /// <summary>
    /// Denotes a binding between two <see cref="Bindable{T}"/> objects
    /// </summary>
    /// <typeparam name="T">The type of our stored <see cref="Bindable{T}.Value"/></typeparam>
    public abstract class Binding<T>
    {
        /// <summary>
        /// A binding source
        /// </summary>
        protected readonly WeakReference<Bindable<T>> Source;

        /// <summary>
        /// A binding target
        /// </summary>
        protected readonly WeakReference<Bindable<T>> Target;

        /// <summary>
        ///  Creates a new <see cref="Binding{T}"/> instance.
        /// </summary>
        /// <param name="source">A binding source</param>
        /// <param name="target">A binding target</param>
        protected Binding(Bindable<T> source, Bindable<T> target)
        {
            Source = new WeakReference<Bindable<T>>(source);
            Target = new WeakReference<Bindable<T>>(target);

            target.Value = source.Value;
            target.Default = source.Default;
            target.Disabled = source.Disabled;
        }

        public abstract void PropagateValueChange(T previousValue, T value, bool bypassChecks, Bindable<T> source);

        public abstract void PropagateDefaultChange(T previousValue, T value, bool bypassChecks, Bindable<T> source);

        public abstract void PropagateDisabledChange(Bindable<T> source, bool propagateToBindings, bool bypassChecks);
    }
}
