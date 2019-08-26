// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Reflection;

namespace osu.Framework.Testing.Attributes
{
    /// <summary>
    /// Denotes a slider step.
    /// </summary>
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

        public override void AddButton(TestScene testScene, MethodInfo method)
        {
            testScene.AddSliderStep<int>(Description, Min, Max, Start, i => method.Invoke(testScene, new object[] { i }));
        }
    }
}
