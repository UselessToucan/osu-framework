// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Threading;
using NUnit.Framework;
using osu.Framework.MathUtils;
using osu.Framework.Timing;

namespace osu.Framework.Tests.Clocks
{
    [TestFixture]
    public class InterpolatingClockTest
    {
        private TestClock source;
        private InterpolatingFramedClock interpolating;

        [SetUp]
        public void SetUp()
        {
            source = new TestClock();

            interpolating = new InterpolatingFramedClock();
            interpolating.ChangeSource(source);
        }

        [Test]
        public void NeverInterpolatesBackwards()
        {
            Assert.That(interpolating.CurrentTime, Is.EqualTo(source.CurrentTime), "Interpolating should match source time.");
            source.Start();
            Assert.That(interpolating.CurrentTime, Is.EqualTo(source.CurrentTime), "Interpolating should match source time.");
            interpolating.ProcessFrame();

            // test with test clock not elapsing
            double lastValue = interpolating.CurrentTime;
            for (int i = 0; i < 100; i++)
            {
                interpolating.ProcessFrame();
                Assert.That(interpolating.CurrentTime, Is.GreaterThanOrEqualTo(lastValue), "Interpolating should not jump against rate.");
                Assert.That(interpolating.CurrentTime, Is.GreaterThanOrEqualTo(source.CurrentTime), "Interpolating should not jump before source time.");

                Thread.Sleep((int)(interpolating.AllowableErrorMilliseconds / 2));
            }

            // test with test clock elapsing
            lastValue = interpolating.CurrentTime;
            for (int i = 0; i < 100; i++)
            {
                source.CurrentTime += 50;
                interpolating.ProcessFrame();

                Assert.That(interpolating.CurrentTime, Is.GreaterThanOrEqualTo(lastValue), "Interpolating should not jump against rate.");
                Assert.That(Math.Abs(interpolating.CurrentTime - source.CurrentTime), Is.LessThanOrEqualTo(interpolating.AllowableErrorMilliseconds), "Interpolating should be within allowance.");

                Thread.Sleep((int)(interpolating.AllowableErrorMilliseconds / 2));
            }
        }

        [Test]
        public void CanSeekBackwards()
        {
            Assert.That(interpolating.CurrentTime, Is.EqualTo(source.CurrentTime), "Interpolating should match source time.");
            source.Start();

            Assert.That(interpolating.CurrentTime, Is.EqualTo(source.CurrentTime), "Interpolating should match source time.");
            interpolating.ProcessFrame();

            source.Seek(10000);
            interpolating.ProcessFrame();
            Assert.That(interpolating.CurrentTime, Is.EqualTo(source.CurrentTime), "Interpolating should match source time.");

            source.Seek(0);
            interpolating.ProcessFrame();
            Assert.That(interpolating.CurrentTime, Is.EqualTo(source.CurrentTime), "Interpolating should match source time.");
        }

        [Test]
        public void InterpolationStaysWithinBounds()
        {
            source.Start();

            const int sleep_time = 20;

            for (int i = 0; i < 100; i++)
            {
                source.CurrentTime += sleep_time;
                interpolating.ProcessFrame();

                Assert.That(Precision.AlmostEquals(interpolating.CurrentTime, source.CurrentTime, interpolating.AllowableErrorMilliseconds),
                    "Interpolating should be within allowable error bounds.");

                Thread.Sleep(sleep_time);
            }

            source.Stop();
            interpolating.ProcessFrame();

            Assert.That(interpolating.IsRunning, Is.False);
            Assert.That(interpolating.CurrentTime, Is.EqualTo(source.CurrentTime), "Interpolating should match source time.");
        }
    }
}
