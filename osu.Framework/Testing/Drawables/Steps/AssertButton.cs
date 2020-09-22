// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Diagnostics;
using osu.Framework.Testing.Asserts;
using osuTK.Graphics;

namespace osu.Framework.Testing.Drawables.Steps
{
    public class AssertButton : StepButton
    {
        public Assert Assertion;
        public string ExtendedDescription;
        public StackTrace CallStack;

        public AssertButton()
        {
            Action += checkAssert;
            LightColour = Color4.OrangeRed;
        }

        private void checkAssert()
        {
            Assertion.Evaluate();
            Success();
        }

        public override string ToString() => "Assert: " + base.ToString();

        private class TracedException : Exception
        {
            private readonly StackTrace trace;

            public TracedException(string description, StackTrace trace)
                : base(description)
            {
                this.trace = trace;
            }

            public override string StackTrace => trace.ToString();
        }
    }
}
