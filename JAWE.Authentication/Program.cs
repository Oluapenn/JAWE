using System;
using JAWE.Authentication.Messaging;
using JAWE.Network.Messaging;
using JAWE.Network.Messaging.Processing;

namespace JAWE.Authentication
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "JAWE.Authentication";

            //Console.WriteLine("Initialized MessageProcessor with {0} messages.", MessageProcessor.MessagesCount);
            Console.WriteLine("Initialized MessageTable with {0} handlers.", LoginClient.MessageTable.HandlerCount);

            var server = new LoginServer();

            server.Activate();

            Console.WriteLine("Hello World!");

            // TODO Keep main thread active until the server is fully shutdown.
            while(true)
                Console.ReadKey(true);
        }
    }
}