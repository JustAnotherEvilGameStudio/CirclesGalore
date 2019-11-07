// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.

using osu.Framework.Bindables;
using osu.Framework.Screens;

namespace Circles.Game.Screens
{
    public abstract class CirclesScreen : Screen, ICirclesScreen, IHasDescription
    {
        /// <summary>
        /// The amount of negative padding that should be applied to game background content which touches both the left and right sides of the screen.
        /// This allows for the game content to be pushed byt he options/notification overlays without causing black areas to appear.
        /// </summary>
        public const float HORIZONTAL_OVERFLOW_PADDING = 50;

        /// <summary>
        /// A user-facing title for this screen.
        /// </summary>
        public virtual string Title => GetType().Name;

        public string Description => Title;

        public virtual bool AllowBackButton => true;

        public virtual bool AllowExternalScreenChange => false;

        public virtual bool CursorVisible => true;

        protected new CirclesBaseGame Game => base.Game as CirclesBaseGame;

        public virtual float BackgroundParallaxAmount => 1;
    }
}
