// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using Circles.Game.Graphics.Containers;

namespace Circles.Game.Screens
{
    public class CirclesScreenStack : ScreenStack
    {
        [Cached]
        private BackgroundScreenStack backgroundScreenStack;

        private ParallaxContainer parallaxContainer;

        protected float ParallaxAmount => parallaxContainer.ParallaxAmount;

        public CirclesScreenStack()
        {
            initializeStack();
        }

        public CirclesScreenStack(IScreen baseScreen)
            : base(baseScreen)
        {
            initializeStack();
        }

        private void initializeStack()
        {
            InternalChild = parallaxContainer = new ParallaxContainer
            {
                RelativeSizeAxes = Axes.Both,
                Child = backgroundScreenStack = new BackgroundScreenStack { RelativeSizeAxes = Axes.Both },
            };

            ScreenPushed += onScreenChange;
            ScreenExited += onScreenChange;
        }

        private void onScreenChange(IScreen prev, IScreen next)
        {
            parallaxContainer.ParallaxAmount = ParallaxContainer.DEFAULT_PARALLAX_AMOUNT * ((ICirclesScreen)next)?.BackgroundParallaxAmount ?? 1.0f;
        }
    }
}