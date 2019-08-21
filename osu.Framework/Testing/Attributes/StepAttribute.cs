// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace osu.Framework.Testing.Attributes
{
    public class StepAttribute : Attribute
    {
        public readonly string Description;

        public StepAttribute(string description)
        {
            Description = description;
        }
    }
}
