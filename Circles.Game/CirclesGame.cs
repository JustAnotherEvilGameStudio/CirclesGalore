using System;
using osu.Framework.Allocation;
using osu.Framework.Development;
using osu.Framework.Logging;
using osu.Framework.Graphics;
using Circles.Game.Screens;
using Circles.Game.Configuration;
using Circles.Game.Graphics.Containers;

namespace Circles.Game
{
    public class CirclesGame : CirclesBaseGame
    {
        protected CirclesScreenStack ScreenStack;

        private readonly string[] args;

        public CirclesGame(string[] args = null)
        {
            this.args = args;
        }

        private DependencyContainer dependencies;

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        [BackgroundDependencyLoader]
        private void load()
        {
            if (!Host.IsPrimaryInstance && !DebugUtils.IsDebugBuild)
            {
                Logger.Log(@$"Circles!{CLIENT_STREAM_NAME} does not support multiple running instances.", LoggingTarget.Runtime, LogLevel.Error);
                Environment.Exit(0);
            }

            dependencies.CacheAs(this);
        }

        private ScalingContainer screenContainer;

        protected override void LoadComplete()
        {
            base.LoadComplete();
        }

    }
}
