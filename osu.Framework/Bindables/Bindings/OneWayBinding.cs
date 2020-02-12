// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;

namespace osu.Framework.Bindables.Bindings
{
    public class OneWayBinding<T> : Binding<T>
    {
        public OneWayBinding(Bindable<T> source, Bindable<T> target)
            : base(source, target)
        {
        }

        public override void PropagateValueChange(T previousValue, T value, bool bypassChecks, Bindable<T> source)
        {
            if (Source.TryGetTarget(out var bindingSource) && bindingSource == source && Target.TryGetTarget(out var bindingTarget) && !EqualityComparer<T>.Default.Equals(bindingTarget.Value, value))
                bindingTarget.SetValue(previousValue, value, bypassChecks, source);
        }

        public override void PropagateDefaultChange(Bindable<T> defaultChangeSource)
        {
            if (Source.TryGetTarget(out var bindingSource) && bindingSource == defaultChangeSource && Target.TryGetTarget(out var bindingTarget))
                bindingTarget.Default = bindingSource.Default;
        }

        public override void PropagateDisabledChange(Bindable<T> disabledChangeSource)
        {
            if (Source.TryGetTarget(out var bindingSource) && bindingSource == disabledChangeSource && Target.TryGetTarget(out var bindingTarget))
                bindingTarget.Disabled = bindingSource.Disabled;
        }
    }
}
