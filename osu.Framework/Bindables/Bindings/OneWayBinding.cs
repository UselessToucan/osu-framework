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

        public override void PropagateValueChange(Bindable<T> valueChangeSource)
        {
            if (Source.TryGetTarget(out var bindingSource) && bindingSource == valueChangeSource && Target.TryGetTarget(out var bindingTarget))
                bindingTarget.Value = bindingSource.Value;
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
