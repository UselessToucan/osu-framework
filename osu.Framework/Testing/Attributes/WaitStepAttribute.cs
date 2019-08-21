// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Reflection;

namespace osu.Framework.Testing.Attributes
{
    public class WaitStepAttribute : StepAttribute
    {
        public readonly int WaitCount;

        public WaitStepAttribute(string description, int waitCount)
            : base(description)
        {
            WaitCount = waitCount;
        }

        public override void AddButton(TestScene testScene, MethodInfo method)
        {
            testScene.AddWaitStep(Description, WaitCount);
        }
    }
}
