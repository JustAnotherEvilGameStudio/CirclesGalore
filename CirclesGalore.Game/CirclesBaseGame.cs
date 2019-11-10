// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.

using System;
using System.Reflection;
using Circles.Game.Configuration;
using Circles.Game.Graphics;
using Circles.Game.Graphics.Containers;
using Circles.Game.Graphics.Cursor;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Development;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Performance;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;
using OGame = osu.Framework.Game;

namespace Circles.Game
{
    public class CirclesBaseGame : OGame
    {
        public const string CLIENT_STREAM_NAME = "Galore";

        protected GameConfigManager LocalConfig;

        protected MenuCursorContainer MenuCursorContainer;

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
            Resources.AddStore(new DllResourceStore(@"osu.Game.Resources.dll"));

            var largeStore = new LargeTextureStore(Host.CreateTextureLoaderStore(new NamespacedResourceStore<byte[]>(Resources, @"Textures")));
            largeStore.AddStore(Host.CreateTextureLoaderStore(new OnlineStore()));
            dependencies.Cache(largeStore);

            dependencies.CacheAs(this);
            dependencies.Cache(LocalConfig);

            dependencies.Cache(new CirclesColour());

            MenuCursorContainer = new MenuCursorContainer { RelativeSizeAxes = Axes.Both };

            base.Content.Add(new ScalingContainer(ScalingMode.Everything) { Child = MenuCursorContainer });
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
