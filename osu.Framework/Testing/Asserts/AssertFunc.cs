// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace osu.Framework.Testing.Asserts
{
    public class AssertFunc : Assert
    {
        private readonly Func<bool> func;

        public AssertFunc(string description, Func<bool> func)
            : base(description)
        {
            this.func = func;
        }

        protected override bool EvaluateInternal() => func();
    }
}
