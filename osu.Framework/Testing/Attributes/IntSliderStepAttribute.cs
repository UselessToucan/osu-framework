﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace osu.Framework.Testing.Attributes
{
    public class IntSliderStepAttribute : StepAttribute
    {
        public readonly int Min;
        public readonly int Max;
        public readonly int Start;

        public IntSliderStepAttribute(string description, int min, int max, int start)
            : base(description)
        {
            Min = min;
            Max = max;
            Start = start;
        }
    }
}
