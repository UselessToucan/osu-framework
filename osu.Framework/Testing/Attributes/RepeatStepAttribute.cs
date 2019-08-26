// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Reflection;

namespace osu.Framework.Testing.Attributes
{
    /// <summary>
    /// Denotes a repeat test step.
    /// </summary>
    public class RepeatStepAttribute : StepAttribute
    {
        public readonly int InvocationCount;

        public RepeatStepAttribute(string description, int invocationCount)
            : base(description)
        {
            InvocationCount = invocationCount;
        }

        public override void AddButton(TestScene testScene, MethodInfo method)
        {
            testScene.AddRepeatStep(Description, () => method.Invoke(testScene, Array.Empty<object>()), InvocationCount);
        }
    }
}
