// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace osu.Framework.Testing.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class VisualTestCategoryAttribute : Attribute
    {
        public readonly string Name;

        public VisualTestCategoryAttribute(string name)
        {
            Name = name;
        }
    }
}
