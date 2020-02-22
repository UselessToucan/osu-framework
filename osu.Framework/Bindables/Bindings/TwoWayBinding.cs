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
                if (!ReferenceEquals(bindingSource, source) && !EqualityComparer<T>.Default.Equals(bindingSource.Value, value))
                    bindingSource.SetValue(previousValue, value, bypassChecks, source);
                else if (!ReferenceEquals(bindingTarget, source) && !EqualityComparer<T>.Default.Equals(bindingTarget.Value, value))
                    bindingTarget.SetValue(previousValue, value, bypassChecks, source);
            }
        }

        public override void PropagateDefaultChange(T previousValue, T value, bool bypassChecks, Bindable<T> source)
        {
            if (Source.TryGetTarget(out var bindingSource) && Target.TryGetTarget(out var bindingTarget))
            {
                var b = !ReferenceEquals(bindingSource, source);
                var b1 = !EqualityComparer<T>.Default.Equals(bindingSource.Default, value);

                if (b && b1)
                    bindingSource.SetDefaultValue(previousValue, value, bypassChecks, source);
                else
                {
                    var b2 = !ReferenceEquals(bindingTarget, source);
                    var b3 = !EqualityComparer<T>.Default.Equals(bindingTarget.Default, value);
                    if (b2 && b3)
                        bindingTarget.SetDefaultValue(previousValue, value, bypassChecks, source);
                }
            }
        }

        public override void PropagateDisabledChange(Bindable<T> source, bool propagateToBindings, bool bypassChecks)
        {
            if (Source.TryGetTarget(out var bindingSource) && Target.TryGetTarget(out var bindingTarget))
            {
                var b = !ReferenceEquals(bindingSource, source);
                var b1 = bindingSource.Disabled != source.Disabled;

                if (b && b1)
                    bindingSource.SetDisabled(source.Disabled, bypassChecks, source);
                else
                {
                    var b2 = !ReferenceEquals(bindingTarget, source);
                    var b3 = bindingTarget.Disabled != source.Disabled;
                    if (b2 && b3)
                        bindingTarget.SetDisabled(source.Disabled, bypassChecks, source);
                }
            }
        }
    }
}
