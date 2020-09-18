// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace osu.Framework.Testing.Asserts
{
    public abstract class Assert<T>
    {
        public readonly string Description;
        public readonly T Actual;
        public readonly T Expected;

        protected Assert(string description, ref T actual, ref T expected)
        {
            Description = description;
            Actual = actual;
            Expected = expected;
        }

        public void Evaluate()
        {
            if (!EvaluateInternal())
                throw new AssertionException($"{Description} assertion failed.{Environment.NewLine}Expected:{Expected}{Environment.NewLine}Actual: {Actual}");
        }

        protected abstract bool EvaluateInternal();
    }
}
