// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Reflection;

namespace osu.Framework.Testing.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class StepAttribute : Attribute
    {
        public readonly string Description;

        public StepAttribute(string description)
        {
            Description = description;
        }

        public virtual void AddButton(TestScene testScene, MethodInfo method)
        {
            testScene.AddStep(Description, () => method.Invoke(testScene, Array.Empty<object>()));
        }
    }
}
