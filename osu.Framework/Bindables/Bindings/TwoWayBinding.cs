// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace osu.Framework.Bindables.Bindings
{
    public class TwoWayBinding<T> : Binding<T>
    {
        public TwoWayBinding(Bindable<T> source, Bindable<T> target)
            : base(source, target)
        {
        }

        public override void PropagateValueChange(Bindable<T> valueChangeSource)
        {
            if (Source.TryGetTarget(out var bindingSource) && Target.TryGetTarget(out var bindingTarget))
            {
                if (bindingSource != valueChangeSource)
                    bindingSource.Value = valueChangeSource.Value;
                else if (bindingTarget != valueChangeSource)
                    bindingTarget.Value = valueChangeSource.Value;
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
