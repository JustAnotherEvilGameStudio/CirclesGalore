using System;
using System.Linq;
using CirclesGalore.Game;
using CirclesGalore.Game.Tests;
using osu.Framework.Platform;
using osu.Framework;
using OGame = osu.Framework.Game;

namespace CirclesGalore.Runner.Desktop
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var testMode = false;

            switch (args.FirstOrDefault() ?? string.Empty)
            {
                case "--test":
                    testMode = true;
                    break;
                default:
                    testMode = false;
                    break;
            }

            using (DesktopGameHost host = Host.GetSuitableHost(@"CirclesGalore"))
            {
                using (OGame game = new CirclesGaloreGame())
                using (OGame gameTests = new CirclesGaloreGameTests())
                {
                    if (testMode) host.Run(gameTests);
                    else host.Run(game);
                }
            }
        }
    }
}
