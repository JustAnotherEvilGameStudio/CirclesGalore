// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.

using System;
using Circles.Game;
using osu.Framework.Platform;
using osu.Framework;
using System.Threading;
using osu.Framework.Development;
using osu.Framework.Logging;
using System.Threading.Tasks;

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
                host.ExceptionThrown += handleException;

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

        private static int allowableExceptions = DebugUtils.IsDebugBuild ? 0 : 1;

        /// <summary>
        /// Allow a maximum of one unhandled exception, per second of execution.
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private static bool handleException(Exception arg)
        {
            bool continueExecution = Interlocked.Decrement(ref allowableExceptions) >= 0;

            Logger.Log($"Unhandled exception has been {(continueExecution ? $"allowed with {allowableExceptions} more allowable exceptions" : "denied")} .");

            // restore the stock of allowable exceptions after a short delay.
            Task.Delay(1000).ContinueWith(_ => Interlocked.Increment(ref allowableExceptions));

            return continueExecution;
        }
    }
}
