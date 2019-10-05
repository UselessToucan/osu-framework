﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using osu.Framework.Testing.Input;

namespace osu.Framework.Testing
{
    /// <summary>
    /// An abstract <see cref="TestSuite"/> which is performed with manual input management.
    /// </summary>
    public abstract class ManualInputManagerTestSuite<T> : TestSuite<T> where T : ManualInputManagerTestScene, new()
    {
        [SetUp]
        public virtual void SetUp() => Scene.ResetInput();

        /// <summary>
        /// The <see cref="ManualInputManager"/>.
        /// </summary>
        public ManualInputManager InputManager => Scene.InputManager;
    }
}
