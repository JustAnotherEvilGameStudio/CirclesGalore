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
        ScreenshotFormat
    }
}
