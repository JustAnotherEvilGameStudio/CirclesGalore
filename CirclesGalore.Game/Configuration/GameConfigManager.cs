using osu.Framework.Configuration;
using osu.Framework.Platform;

namespace Circles.Game.Configuration
{
    public class GameConfigManager : IniConfigManager<CirclesSetting>
    {
        protected override void InitialiseDefaults()
        {
            Set(CirclesSetting.ShowFpsDisplay, true);

            Set(CirclesSetting.Version, string.Empty);

            Set(CirclesSetting.ScreenshotFormat, ScreenshotFormat.Png);
            Set(CirclesSetting.ScreenshotCaptureMenuCursor, false);

            Set(CirclesSetting.CursorRotation, true);
            Set(CirclesSetting.MenuCursorSize, 1.0f, 0.5f, 2f, 0.01f);

            Set(CirclesSetting.Scaling, ScalingMode.Off);

            Set(CirclesSetting.ScalingSizeX, 0.8f, 0.2f, 1f);
            Set(CirclesSetting.ScalingSizeY, 0.8f, 0.2f, 1f);

            Set(CirclesSetting.ScalingPositionX, 0.5f, 0f, 1f);
            Set(CirclesSetting.ScalingPositionY, 0.5f, 0f, 1f);

            Set(CirclesSetting.UIScale, 1f, 0.8f, 1.6f, 0.01f);
        }

        public GameConfigManager(Storage storage)
            : base(storage)
        {
        }
    }

    public enum CirclesSetting
    {
        ShowFpsDisplay,

        Version,

        ScreenshotCaptureMenuCursor,
        ScreenshotFormat,

        CursorRotation,
        MenuCursorSize,

        Scaling,
        ScalingPositionX,
        ScalingPositionY,
        ScalingSizeX,
        ScalingSizeY,
        UIScale
    }
}
