// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.

using Circles.Game.Graphics.Backgrounds;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.MathUtils;
using osu.Framework.Threading;

namespace Circles.Game.Screens.Backgrounds
{
    public class BackgroundScreenDefault : BackgroundScreen
    {
        private Background background;

        private int currentDisplay;
        private const int background_count = 5;

        private string backgroundName => $@"Menu/menu-background-{currentDisplay % background_count + 1}";

        [BackgroundDependencyLoader]
        private void load()
        {

            currentDisplay = RNG.Next(0, background_count);

            display(createBackground());
        }

        private void display(Background newBackground)
        {
            background?.FadeOut(800, Easing.InOutSine);
            background?.Expire();

            AddInternal(background = newBackground);
            currentDisplay++;
        }

        private ScheduledDelegate nextTask;

        public void Next()
        {
            nextTask?.Cancel();
            nextTask = Scheduler.AddDelayed(() => { LoadComponentAsync(createBackground(), display); }, 100);
        }

        private Background createBackground()
        {
            Background newBackground = new Background(backgroundName);

            newBackground.Depth = currentDisplay;

            return newBackground;
        }
    }
}