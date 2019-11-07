using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Circles.Game.Graphics;
using Circles.Game.Input.Bindings;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Input.Bindings;
using osu.Framework.Threading;

namespace Circles.Game
{
    public class CirclesGame : CirclesBaseGame, IKeyBindingHandler<GlobalAction>
    {
        [Cached]
        private readonly ScreenshotManager screenshotManager = new ScreenshotManager();

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
            dependencies.CacheAs(this);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            loadComponentSingleFile(screenshotManager, Add);
        }

        private Task asyncLoadStream;

        private T loadComponentSingleFile<T>(T d, Action<T> add, bool cache = false)
            where T : Drawable
        {
            if (cache)
                dependencies.Cache(d);

            Schedule(() =>
            {
                var previousLoadStream = asyncLoadStream;

                asyncLoadStream = Task.Run(async () =>
                {
                    if (previousLoadStream != null)
                        await previousLoadStream;

                    try
                    {
                        Task task = null;
                        var del = new ScheduledDelegate(() => task = LoadComponentAsync(d, add));
                        Scheduler.Add(del);

                        while (!IsDisposed && !del.Completed)
                            await Task.Delay(10);

                        if (IsDisposed)
                            return;

                        Debug.Assert(task != null);

                        await task;
                    }
                    catch (OperationCanceledException)
                    {
                    }
                });
            });

            return d;
        }


        public bool OnPressed(GlobalAction action)
        {
            // if (introScreen == null) return false;

            /* switch (action)
            {
            } */

            return false;
        }

        public bool OnReleased(GlobalAction action) => false;

    }
}
