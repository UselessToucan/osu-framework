// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;

namespace osu.Framework.Bindables.Bindings
{
    public class TwoWayBinding<T> : Binding<T>
    {
        public TwoWayBinding(Bindable<T> source, Bindable<T> target)
            : base(source, target)
        {
        }

        public override void PropagateValueChange(T previousValue, T value, bool bypassChecks, Bindable<T> source)
        {
            if (Source.TryGetTarget(out var bindingSource) && Target.TryGetTarget(out var bindingTarget))
            {
                if (bindingSource != source && !EqualityComparer<T>.Default.Equals(bindingSource.Value, value))
                    bindingSource.SetValue(previousValue, value, bypassChecks, source);
                else if (bindingTarget != source && !EqualityComparer<T>.Default.Equals(bindingTarget.Value, value))
                    bindingTarget.SetValue(previousValue, value, bypassChecks, source);
            }
        }

        public override void PropagateDefaultChange(T previousValue, T value, bool bypassChecks, Bindable<T> source)
        {
            if (Source.TryGetTarget(out var bindingSource) && Target.TryGetTarget(out var bindingTarget))
            {
                if (bindingSource != source && !EqualityComparer<T>.Default.Equals(bindingSource.Default, value))
                    bindingSource.SetDefaultValue(previousValue, value, bypassChecks, source);
                else if (bindingTarget != source && !EqualityComparer<T>.Default.Equals(bindingTarget.Default, value))
                    bindingTarget.SetDefaultValue(previousValue, value, bypassChecks, source);
            }
        }

        public override void PropagateDisabledChange(Bindable<T> source, bool propagateToBindings, bool bypassChecks)
        {
            if (Source.TryGetTarget(out var bindingSource) && Target.TryGetTarget(out var bindingTarget))
            {
                if (bindingSource != source)
                    bindingSource.SetDisabled(source.Disabled, bypassChecks, source);
                else if (bindingTarget != source)
                    bindingTarget.SetDisabled(source.Disabled, bypassChecks, source);
            }
        }
    }
}
