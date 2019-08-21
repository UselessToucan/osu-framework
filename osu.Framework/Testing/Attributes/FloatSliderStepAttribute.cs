// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace osu.Framework.Testing.Attributes
{
    public class FloatSliderStepAttribute : StepAttribute
    {
        public readonly float Min;
        public readonly float Max;
        public readonly float Start;

        public FloatSliderStepAttribute(string description, float min, float max, float start)
            : base(description)
        {
            Min = min;
            Max = max;
            Start = start;
        }
    }
}
