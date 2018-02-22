using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using JAWE.Game.Messaging.Builders;

namespace JAWE.Game.Lobby
{
    internal static class HearthBeat
    {
        public static readonly int Internal = 5000;
        public static bool Active;

        public static void Initialize()
        {
            if (Active)
                return;

            Active = true;
            Task.Run(() => Process());
        }

        private static void Process()
        {
            var stopWatch = new Stopwatch();
            while (Active)
            {
                stopWatch.Restart();

                // Send a hearth beat message to all clients inside all channels.
                foreach (var channel in Channels.All())
                {
                    foreach (var client in channel.Clients())
                    {
                        client.Send(KeepaliveBuilder.Build(client));
                    }
                }

                stopWatch.Stop();

                // Keep the thread in sync with the internal.
                var sleepTime = Internal - (int)stopWatch.ElapsedMilliseconds;
                if (sleepTime > 0)
                {
                    Thread.Sleep(sleepTime);
                }
                else
                {
                    Console.WriteLine("Hearthbeat couldn't keep-up!!");
                }
            }
        }

    }
}
