// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Reflection;

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

        public override void AddButton(TestScene testScene, MethodInfo method)
        {
            testScene.AddAssert(Description, () => (bool)method.Invoke(testScene, Array.Empty<object>()), ExtendedDescription);
        }
    }
}
