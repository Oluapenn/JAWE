using System;
using System.Net;
using System.Net.Sockets;

namespace JAWE.Network
{
    public abstract class Server
    {
        public ushort Port { get; set; }

        public bool IsShutdown => Socket == null;

        protected Socket Socket { get; private set; }

        protected Server(ProtocolType protocolType, ushort port)
        {
            var socketType = protocolType == ProtocolType.Tcp
                ? SocketType.Stream
                : SocketType.Dgram;

            Port = port;

            try
            {
                Socket = new Socket(AddressFamily.InterNetwork, socketType, protocolType);
            }
            catch (Exception e)
            {
                Socket = null;
                Console.WriteLine(e.ToString());
            }
        }

        public abstract void Activate();

        public void Shutdown()
        {
            if (IsShutdown)
                return;

            Socket.Shutdown(SocketShutdown.Both);
            Socket.Dispose();
            Socket = null;
        }
    }
}