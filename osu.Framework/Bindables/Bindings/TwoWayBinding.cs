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
    }
}
