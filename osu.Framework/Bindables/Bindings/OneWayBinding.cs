// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace osu.Framework.Bindables.Bindings
{
    public class OneWayBinding<T> : Binding<T>
    {
        public OneWayBinding(Bindable<T> source, Bindable<T> target)
            : base(source, target)
        {
            if (Source.TryGetTarget(out var sourceObj))
                sourceObj.ValueChanged += Source_ValueChanged;
        }

        private void Source_ValueChanged(ValueChangedEvent<T> source)
        {
            if (Target.TryGetTarget(out var target))
                target.Value = source.NewValue;
        }
    }
}
