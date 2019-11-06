using System;
using CirclesGalore.Game;
using CirclesGalore.Game.Tests;
using osu.Framework.Platform;
using osu.Framework;
using OGame = osu.Framework.Game;

namespace CirclesGalore.Runner.Desktop
{
    class Program
    {
        [MTAThread]
        public static void Main()
        {
            var testMode = true;

            using (GameHost host = Host.GetSuitableHost(@"CirclesGalore"))
            using (OGame game = new CirclesGaloreGame())
            using (OGame gameTests = new CirclesGaloreGameTests())
            {
                if (testMode) host.Run(gameTests);
                else host.Run(game);
            }
        }
    }
}
