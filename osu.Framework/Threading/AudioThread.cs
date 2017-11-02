﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System;
using System.Collections.Generic;
using ManagedBass;
using osu.Framework.Statistics;

namespace osu.Framework.Threading
{
    public class AudioThread : GameThread
    {
        public AudioThread(Action onNewFrame, string threadName)
            : base(onNewFrame, threadName)
        {
        }

        internal override IEnumerable<StatisticsCounterType> StatisticsCounters => new[]
        {
            StatisticsCounterType.TasksRun,
            StatisticsCounterType.Tracks,
            StatisticsCounterType.Samples,
            StatisticsCounterType.SChannels,
            StatisticsCounterType.Components,
        };

        public override void Exit()
        {
            base.Exit();

            Bass.Free();
        }
    }
}
