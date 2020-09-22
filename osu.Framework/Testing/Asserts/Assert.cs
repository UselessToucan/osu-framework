// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace osu.Framework.Testing.Asserts
{
    public abstract class Assert
    {
        public readonly string Description;

        protected virtual string FailureMessage => $"{Description} assertion failed.";

        protected Assert(string description)
        {
            Description = description;
        }

        public void Evaluate()
        {
            if (!EvaluateInternal())
                throw new AssertionException(FailureMessage);
        }

        protected abstract bool EvaluateInternal();
    }
}
