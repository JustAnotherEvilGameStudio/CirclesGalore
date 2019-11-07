using System;
using Circles.Game;
using osu.Framework.Platform;
using osu.Framework;

namespace Circles.Desktop
{
    class Program
    {

        [STAThread]
        public static void Main(string[] args)
        {
            foreach (string arg in args)
                ParseFlags(arg);

            using (DesktopGameHost host = Host.GetSuitableHost(@"Circles!Galore"))
            {
                host.Run(new CirclesGame());
            }
        }

        private static void ParseFlags(string arg)
        {
            switch (arg ?? string.Empty)
            {
                default:
                    break;
            }
        }
    }
}
