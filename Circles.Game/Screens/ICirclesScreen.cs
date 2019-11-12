using osu.Framework.Screens;

namespace Circles.Game.Screens
{
    public interface ICirclesScreen : IScreen
    {
        bool AllowBackButton { get; }

        bool AllowExternalScreenChange { get; }

        bool CursorVisible { get; }

        float BackgroundParallaxAmount { get; }
    }
}
