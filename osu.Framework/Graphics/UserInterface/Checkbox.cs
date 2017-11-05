﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using osu.Framework.Configuration;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input;

namespace osu.Framework.Graphics.UserInterface
{
    public abstract class Checkbox : Container, IHasCurrentValue<bool>, IHandleOnClick
    {
        public Bindable<bool> Current { get; } = new Bindable<bool>();

        public virtual bool OnClick(InputState state)
        {
            if (!Current.Disabled)
                Current.Value = !Current;

            return true;
        }
    }
}
