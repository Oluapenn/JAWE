using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace JAWE.Network
{
    public abstract class Client
    {
        private static readonly object GlobalSentObjectLock = new object();
        private static readonly object GlobalRecvObjectLock = new object();

        public static ulong GlobalTotalBytesSent;
        public static ulong GlobalTotalBytesReceived;

        private Socket _socket;
        private byte[] _receiveBuffer;


        public uint ConnectionId { get; private set; }
        public ushort ConnectionSlot { get; private set; }
        public bool IsDisconnected { get; private set; }
        public uint BytesSent { get; set; }
        public uint BytesReceived { get; set; }
        
        public void Initialize(uint connectionId, ushort connectionSlot, Socket socket)
        {
            ConnectionId = connectionId;
            ConnectionSlot = connectionSlot;
            IsDisconnected = false;

            // Configure Socket
            _receiveBuffer = new byte[0x8000];
            _socket = socket;
            _socket.NoDelay = true;

            // Add receive and send time-out in non-debug builds.
#if !DEBUG
            _socket.ReceiveTimeout = 15000;
            _socket.SendTimeout = 15000;
#endif

            // Start Listening
            Receive();

            // Invoke The Initialized Event.
            Initialized?.Invoke(this, new EventArgs());
        }

        private void Receive()
        {
            Task.Run(() =>
            {
                int bytesReceived;

                try
                {
                    bytesReceived = _socket.Receive(_receiveBuffer, SocketFlags.None);
                }
                catch (Exception)
                {
                    bytesReceived = 0;
                }

                if (bytesReceived <= 0)
                {
                    Disconnect("Socket received zero bytes.");
                    return;
                }

                // Increase received bytes information.
                BytesReceived += (uint) bytesReceived;
                lock (GlobalRecvObjectLock)
                {
                    GlobalTotalBytesReceived += (uint) bytesReceived;
                }

                var receivedBuffer = new byte[bytesReceived];
                Array.Copy(_receiveBuffer, receivedBuffer, receivedBuffer.Length);

                DataReceived?.Invoke(this, new DataReceivedEventArgs(receivedBuffer));

                Receive();
            });
        }
        
        public void Send(byte[] buffer)
        {
            Task.Run(() =>
            {
                int  bytesSent;
                try
                {
                    bytesSent = _socket.Send(buffer, SocketFlags.None);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    Disconnect("Error while sending data.");
                    return;
                }

                // Increase bytes sent information.
                BytesSent += (uint) bytesSent;
                lock (GlobalSentObjectLock)
                {
                    GlobalTotalBytesSent += (uint) bytesSent;
                }
            });
        }

        public void Disconnect()
        {
            Disconnect("Forced Disconnected");
        }

        public void Disconnect(string reason)
        {
            if (IsDisconnected)
                return;

            IsDisconnected = true;

            Console.WriteLine("Client disconnected, reason: {0}", reason);

            Disconnected?.Invoke(this, new DisconnectedEventArgs(reason));

            _socket.Shutdown(SocketShutdown.Both);
            _socket.Dispose();
            _socket = null;
        }

        #region Events

        public EventHandler<DisconnectedEventArgs> Disconnected;
        public EventHandler<DataReceivedEventArgs> DataReceived;
        public EventHandler Initialized;

        #endregion
    }
}