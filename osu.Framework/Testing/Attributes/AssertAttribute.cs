// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace osu.Framework.Testing.Attributes
{
    public class AssertAttribute : StepAttribute
    {
        public readonly string ExtendedDescription;

        public AssertAttribute(string description, string extendedDescription = null)
            : base(description)
        {
            ExtendedDescription = extendedDescription;
        }
    }
}
