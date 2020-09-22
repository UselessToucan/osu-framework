// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace osu.Framework.Testing.Asserts
{
    public abstract class ValueBasedAssert<T> : Assert
    {
        public readonly T Actual;
        public readonly T Expected;

        protected override string FailureMessage => $"{Description} assertion failed.{Environment.NewLine}Expected:{Expected}{Environment.NewLine}Actual: {Actual}";

        protected ValueBasedAssert(string description, T actual, T expected)
            : base(description)
        {
            Actual = actual;
            Expected = expected;
        }
    }
}
