// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Reflection;

namespace osu.Framework.Testing.Attributes
{
    /// <summary>
    /// Denotes a test label.
    /// </summary>
    public class LabelAttribute : StepAttribute
    {
        public LabelAttribute(string description)
            : base(description)
        {
        }

        public override void AddButton(TestScene testScene, MethodInfo method)
        {
            testScene.AddLabel(Description);
        }
    }
}
