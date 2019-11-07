// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.

using System;
using System.Reflection;
using Circles.Game.Configuration;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Development;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Performance;
using osu.Framework.Platform;
using OGame = osu.Framework.Game;

namespace Circles.Game
{
    public class CirclesBaseGame : OGame
    {
        protected GameConfigManager LocalConfig;

        private Container content;

        protected override Container<Drawable> Content => content;

        protected Storage Storage { get; set; }

        private Bindable<bool> fpsDisplayVisible;

        public virtual Version AssemblyVersion
            => Assembly.GetEntryAssembly()?.GetName().Version ?? new Version();

        public bool IsDeployedBuild
            => AssemblyVersion.Major > 0;

        public string Version
        {
            get
            {
                if (!IsDeployedBuild) return @"local " + (DebugUtils.IsDebugBuild ? @"debug" : @"release");

                var version = AssemblyVersion;
                return $@"{version.Major}.{version.Minor}.{version.Build}";
            }
        }

        public CirclesBaseGame()
        {
            Name = @"Circles!Galore";
        }

        private DependencyContainer dependencies;

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        [BackgroundDependencyLoader]
        private void load()
        {
            dependencies.CacheAs(this);
            dependencies.Cache(LocalConfig);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            fpsDisplayVisible = LocalConfig.GetBindable<bool>(CirclesSetting.ShowFpsDisplay);
            fpsDisplayVisible.ValueChanged += visible => { FrameStatistics.Value = visible.NewValue ? FrameStatisticsMode.Minimal : FrameStatisticsMode.None; };
            fpsDisplayVisible.TriggerChange();

            FrameStatistics.ValueChanged += e => fpsDisplayVisible.Value = e.NewValue != FrameStatisticsMode.None;
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            if (Storage == null)
                Storage = host.Storage;

            if (LocalConfig == null)
                LocalConfig = new GameConfigManager(Storage);
        }
    }
}
