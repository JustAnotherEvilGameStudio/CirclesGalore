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

        private static bool _Test { get; set; }

        [STAThread]
        public static void Main(string[] args)
        {
            foreach (string arg in args)
                ParseFlags(arg);

            using (DesktopGameHost host = Host.GetSuitableHost(@"CirclesGalore"))
            {
                using (OGame game = new CirclesGaloreGame())
                using (OGame gameTests = new CirclesGaloreGameTests())
                {
                    if (_Test) host.Run(gameTests);
                    else host.Run(game);
                }
            }
        }

        private static void ParseFlags(string arg)
        {
            switch (arg ?? string.Empty)
            {
                case "--test":
                    _Test = true;
                    break;
                default:
                    _Test = false;
                    break;
            }
        }
    }
}
