// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Reflection;

namespace osu.Framework.Testing.Attributes
{
    public class ToggleStepAttribute : StepAttribute
    {
        public ToggleStepAttribute(string description)
            : base(description)
        {
        }

        public override void AddButton(TestScene testScene, MethodInfo method)
        {
            testScene.AddToggleStep(Description, (b) => method.Invoke(testScene, new object[] { b }));
        }
    }
}
