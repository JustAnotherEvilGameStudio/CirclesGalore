using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Platform;
using osu.Framework.Testing;

namespace Circles.Game.Tests
{
    public class CirclesGameTests : CirclesGame
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Child = new DrawSizePreservingFillContainer
            {
                Children = new Drawable[]
                {
                    new TestBrowser("CirclesGalore"), // lgtm [cs/local-not-disposed]
                    new CursorContainer() // lgtm [cs/local-not-disposed]
                },
            };
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);
            host.Window.CursorState |= CursorState.Hidden;
        }
    }
}
