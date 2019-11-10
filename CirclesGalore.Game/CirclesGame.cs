using osu.Framework.Allocation;

namespace Circles.Game
{
    public class CirclesGame : CirclesBaseGame
    {

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

    }
}
