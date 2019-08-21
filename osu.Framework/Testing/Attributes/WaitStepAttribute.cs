// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace osu.Framework.Testing.Attributes
{
    public class WaitStepAttribute : StepAttribute
    {
        public WaitStepAttribute(string description, int waitCount)
            : base(description)
        {
        }
    }
}
