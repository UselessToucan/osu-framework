// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Testing.Drawables.Steps;
using osuTK.Graphics;

namespace osu.Framework.Testing
{
    public class SetUpStep : SingleStepButton
    {
        public SetUpStep()
        {
            Text = "[SetUp]";
            LightColour = Color4.Teal;
        }
    }
}
