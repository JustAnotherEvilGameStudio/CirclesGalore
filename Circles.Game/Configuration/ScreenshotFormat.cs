// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.

using System.ComponentModel;

namespace Circles.Game.Configuration
{
    public enum ScreenshotFormat
    {
        [Description("JPG (web-friendly)")]
        Jpg = 1,

        [Description("PNG (lossless)")]
        Png = 2,

        [Description("GIF")]
        Gif = 3,

        [Description("Bit Map")]
        Bmp = 4
    }
}
