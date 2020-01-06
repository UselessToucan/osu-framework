﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace osu.Framework.Bindables.Bindings
{
    public class TwoWayBinding<T> : OneWayBinding<T>
    {
        public TwoWayBinding(Bindable<T> source, Bindable<T> target)
            : base(source, target)
        {
            if (Target.TryGetTarget(out var targetObj))
                targetObj.ValueChanged += target_ValueChanged;
        }

        private void target_ValueChanged(ValueChangedEvent<T> target)
        {
            if (Source.TryGetTarget(out var source))
                source.Value = target.NewValue;
        }

        public override void Unbind()
        {
            if (Target.TryGetTarget(out var targetObj))
                targetObj.ValueChanged -= target_ValueChanged;
        }
    }
}
