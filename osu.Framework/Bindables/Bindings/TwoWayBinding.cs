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

        public override void PropagateDefaultChange(Bindable<T> defaultChangeSource)
        {
            if (Source.TryGetTarget(out var bindingSource) && Target.TryGetTarget(out var bindingTarget))
            {
                if (bindingSource != defaultChangeSource)
                    bindingSource.Default = defaultChangeSource.Default;
                else if (bindingTarget != defaultChangeSource)
                    bindingTarget.Default = defaultChangeSource.Default;
            }
        }

        public override void PropagateDisabledChange(Bindable<T> disabledChangeSource)
        {
            if (Source.TryGetTarget(out var bindingSource) && Target.TryGetTarget(out var bindingTarget))
            {
                if (bindingSource != disabledChangeSource)
                    bindingSource.Disabled = disabledChangeSource.Disabled;
                else if (bindingTarget != disabledChangeSource)
                    bindingTarget.Disabled = disabledChangeSource.Disabled;
            }
        }
    }
}
