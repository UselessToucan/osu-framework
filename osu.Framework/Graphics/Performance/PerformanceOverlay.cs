// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System;
using System.Collections.Generic;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.OpenGL;
using osu.Framework.Graphics.Textures;
using osu.Framework.Threading;
using OpenTK.Graphics.ES30;

namespace osu.Framework.Graphics.Performance
{
    internal class PerformanceOverlay : FillFlowContainer<FrameStatisticsDisplay>, IStateful<FrameStatisticsMode>
    {
        private FrameStatisticsMode state;

        public event Action<FrameStatisticsMode> StateChanged;

        public FrameStatisticsMode State
        {
            get { return state; }

            set
            {
                if (state == value) return;

                state = value;

                switch (state)
                {
                    case FrameStatisticsMode.None:
                        this.FadeOut(100);
                        break;
                    case FrameStatisticsMode.Minimal:
                    case FrameStatisticsMode.Full:
                        this.FadeIn(100);
                        break;
                }

                foreach (FrameStatisticsDisplay d in Children)
                    d.State = state;

                StateChanged?.Invoke(State);
            }
        }

        public PerformanceOverlay(IEnumerable<GameThread> threads)
        {
            Direction = FillDirection.Vertical;
            TextureAtlas atlas = new TextureAtlas(GLWrapper.MaxTextureSize, GLWrapper.MaxTextureSize, true, All.Nearest);

            foreach (GameThread t in threads)
                Add(new FrameStatisticsDisplay(t, atlas) { State = state });
        }
    }

    public enum FrameStatisticsMode
    {
        None,
        Minimal,
        Full
    }
}
