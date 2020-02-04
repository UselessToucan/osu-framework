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

        public override void PropagateValueChange(Bindable<T> source)
        {
            if (Source.TryGetTarget(out var bindingSource) && bindingSource == source && Target.TryGetTarget(out var bindingTarget))
                bindingTarget.Value = bindingSource.Value;
        }
    }
}
