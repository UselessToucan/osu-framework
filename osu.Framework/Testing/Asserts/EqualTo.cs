// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace osu.Framework.Testing.Asserts
{
    public class EqualTo<T> : Assert<T> where T : IEquatable<T>
    {
        public EqualTo(string description, ref T actual, ref T expected)
            : base(description, ref actual, ref expected)
        {
        }

        protected override bool EvaluateInternal()
        {
            return Actual.Equals(Expected);
        }
    }
}
