using System;
using JAWE.Game.Lobby;
using JAWE.Game.Messaging;
using JAWE.Network;
using JAWE.Network.Messaging.Processing;

namespace JAWE.Game
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "JAWE.Game";

            //Console.WriteLine("Initialized MessageProcessor with {0} messages.", MessageProcessorOld.MessagesCount);
            Console.WriteLine("Initialized MessageTable with {0} handlers.", GameClient.MessageTable.HandlerCount);

#if !DEBUG
            HearthBeat.Initialize();
#endif

            var server = new GameServer();

            var peerServer = new UdpServer(5350, GameClient.WrEncoder);
            var natServer = new UdpServer(5351, null);

            server.Activate();
            peerServer.Activate();
            natServer.Activate();

            Console.WriteLine("Hello World!");

            // TODO Keep main thread active until the server is fully shutdown.
            while (true)
                Console.ReadKey(true);
        }
    }
}