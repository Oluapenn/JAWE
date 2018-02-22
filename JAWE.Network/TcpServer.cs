using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace JAWE.Network
{
    public abstract class TcpServer<T> : Server where T : Client
    {
        public uint TotalConnections { get; set; }
        public ushort ConnectionLimit { get; set; }
        public int Backlog { get; }

        private readonly Dictionary<ushort, Client> _clients;

        protected TcpServer(ushort port, int backlog)
            : base(ProtocolType.Tcp, port)
        {
            Backlog = backlog;
            TotalConnections = 0;
            ConnectionLimit = ushort.MaxValue - 1;
            _clients = new Dictionary<ushort, Client>();
        }

        public override void Activate()
        {
            Socket.Bind(new IPEndPoint(IPAddress.Any, Port));
            Socket.Listen(Backlog);

            Console.WriteLine("Tcp Server Activated on Port: {0} Backlog: {1}.", Port, Backlog);

            // Begin listening for connections.
            BeginAccept();
        }

        private void BeginAccept()
        {
            Task.Run(() => DoAccept());
        }

        private void DoAccept()
        {
            Socket newSocket = null;

            try
            {
                newSocket = Socket.AcceptAsync().Result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            if (newSocket != null)
            {
                Console.WriteLine("New Connection From: {0}", newSocket.RemoteEndPoint);
                // Search for open connection slot.
                var connectionSlot = FirstOpenSlot();

                if (connectionSlot != ushort.MaxValue)
                {
                    var client = Activator.CreateInstance<T>();
                    client.Disconnected += OnClientDisconnected;
                    client.Initialize(TotalConnections++, connectionSlot, newSocket);
                    Console.WriteLine("Accepted Connection: #{0} Slot: {1}", TotalConnections, connectionSlot);
                }
                else
                {
                    Console.WriteLine("Maximum connections reached; closing connection.");
                    // We did not find an open connection slot.
                    newSocket.Shutdown(SocketShutdown.Both);
                }
            }

            if (!IsShutdown)
            {
                BeginAccept();
            }
        }

        private ushort FirstOpenSlot()
        {
            for (ushort slot = 0; slot < ConnectionLimit; slot++)
            {
                if (!_clients.ContainsKey(slot))
                {
                    return slot;
                }
            }

            return ushort.MaxValue;
        }

        #region Events

        private void OnClientDisconnected(object sender, DisconnectedEventArgs disconnectedEventArgs)
        {
            var client = sender as Client;

            if (client == null)
                return;

            _clients.Remove(client.ConnectionSlot);
            Console.WriteLine("Flagged Connection Slot {0} as free.", client.ConnectionSlot);
        }

        #endregion
    }
}