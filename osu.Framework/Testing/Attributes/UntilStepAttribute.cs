// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Reflection;

namespace osu.Framework.Testing.Attributes
{
    /// <summary>
    /// Denotes an until step.
    /// </summary>
    public class UntilStepAttribute : StepAttribute
    {
        public UntilStepAttribute(string description)
            : base(description)
        {
        }

        public override void AddButton(TestScene testScene, MethodInfo method)
        {
            testScene.AddUntilStep(Description, () => (bool)method.Invoke(testScene, Array.Empty<object>()));
        }
    }
}
