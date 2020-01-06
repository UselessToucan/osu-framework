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
                sourceObj.ValueChanged += source_ValueChanged;
        }

        private void source_ValueChanged(ValueChangedEvent<T> source)
        {
            if (Target.TryGetTarget(out var target))
                target.Value = source.NewValue;
        }

        public override void Unbind()
        {
            if (Source.TryGetTarget(out var sourceObj))
                sourceObj.ValueChanged -= source_ValueChanged;
        }
    }
}
