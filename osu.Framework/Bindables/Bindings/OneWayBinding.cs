// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

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
