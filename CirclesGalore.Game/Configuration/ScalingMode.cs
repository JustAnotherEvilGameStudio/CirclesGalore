// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.

using System.ComponentModel;

namespace Circles.Game.Configuration
{
    public enum ScalingMode
    {
        Off,
        Everything,

        [Description("Excluding overlays")]
        ExcludeOverlays,
        Gameplay,
    }
}
