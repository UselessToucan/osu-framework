// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace osu.Framework.Bindables.Bindings
{
    /// <summary>
    /// A <see cref="Binding{T}"/> implementation where changes are propagated only from <see cref="Binding{T}.Source"/> to <see cref="Binding{T}.Target"/>
    /// </summary>
    /// <typeparam name="T">The type of our stored <see cref="Bindable{T}.Value"/></typeparam>
    public class OneWayBinding<T> : Binding<T>
    {
        /// <summary>
        /// Creates a new <see cref="OneWayBinding{T}"/> instance.
        /// </summary>
        /// <param name="source">A binding source</param>
        /// <param name="target">A binding target</param>
        public OneWayBinding(Bindable<T> source, Bindable<T> target)
            : base(source, target)
        {
        }

        public override void PropagateValueChange(T previousValue, T value, bool bypassChecks, Bindable<T> source)
        {
            if (Source.TryGetTarget(out var bindingSource) && bindingSource == source && Target.TryGetTarget(out var bindingTarget))
                bindingTarget.SetValue(previousValue, value, bypassChecks, source);
        }

        public override void PropagateDefaultChange(T previousValue, T value, bool bypassChecks, Bindable<T> source)
        {
            if (Source.TryGetTarget(out var bindingSource) && bindingSource == source && Target.TryGetTarget(out var bindingTarget))
                bindingTarget.SetDefaultValue(previousValue, value, bypassChecks, source);
        }

        public override void PropagateDisabledChange(Bindable<T> source, bool propagateToBindings, bool bypassChecks)
        {
            if (Source.TryGetTarget(out var bindingSource) && bindingSource == source && Target.TryGetTarget(out var bindingTarget))
                bindingTarget.SetDisabled(source.Disabled, bypassChecks, source);
        }
    }
}
