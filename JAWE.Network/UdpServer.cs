using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using JAWE.Network.Codec;
using JAWE.Network.P2p;
using JAWE.Network.P2p.Messages;

namespace JAWE.Network
{
    public class UdpServer : Server
    {
        private readonly WrMessageCodec _codec;
        private readonly byte[] _receiveBuffer;
        private EndPoint _receiveEndPoint;

        public UdpServer(ushort port, WrMessageCodec codec)
            : base(ProtocolType.Udp, port)
        {
            _codec = codec;
            _receiveBuffer = new byte[0x400];
            _receiveEndPoint = new IPEndPoint(IPAddress.Any, port);
        }

        public override void Activate()
        {
            Socket.Bind(new IPEndPoint(IPAddress.Any, Port));
            Receive();
        }

        private void Receive()
        {
            Task.Run(() =>
            {
                ProcessReceive();
                Receive();
            });
        }

        private void SendTo(byte[] buffer, EndPoint target)
        {
            Task.Run(() =>
            {
                try
                {
                    Socket.SendTo(buffer, SocketFlags.None, target);
                }
                catch (Exception)
                {
                    // ignored
                }
            });
        }

        // TODO Move this to it's own class inside the game server.
        private void ProcessReceive()
        {
            var bytesReceived = Socket.ReceiveFrom(_receiveBuffer, SocketFlags.None, ref _receiveEndPoint);

            if (bytesReceived < 14)
                return;

            // TODO Exception handeling.

            using (var memoryStream = new MemoryStream(_receiveBuffer, 0, bytesReceived))
            using (var reader = new BinaryReader(memoryStream))
            {
                var messageHeader = UdpMessageParser.Parse<MessageHeader>(reader);

                if (messageHeader == null)
                    return;

                if (Port == 5351 && messageHeader.Identity != UdpHeaderType.Connect)
                    return; // Ignore all packets except connect on the NAT socket.
                
                // TODO Validate the IP-Address the message is sent from.
                // TODO Validate if the IP-Address is connected to the server.

                Console.WriteLine("UDP {0} >> {1}", Port, messageHeader);

                switch (messageHeader.Identity)
                {
                    case UdpHeaderType.Connect:
                        // Echo back the connect bytes.
                        // Seed Code in this packet is the user id.
                        reader.BaseStream.Seek(0, SeekOrigin.Begin);

                        var outputMessage = reader.ReadBytes(bytesReceived);
                        SendTo(outputMessage, _receiveEndPoint);
                        break;

                    case UdpHeaderType.Descript:
                        if (bytesReceived < 28)
                            return; // Not enough bytes to parse this message.

                        var descriptHeader = UdpMessageParser.Parse<DescriptMessage>(reader);

                        if (descriptHeader == null)
                            return;

                        if (bytesReceived != descriptHeader.Length)
                        {
                            Console.WriteLine("Error, Descript Length does not match.");
                            return;
                        }

                        Console.WriteLine("Descript Header: {0}", descriptHeader);

                        var decodedContent = ParseAndDecode(reader, descriptHeader.Length - 28);

                        ProcessDescript(messageHeader, descriptHeader, decodedContent);

                        break;

                    default:
                        Console.WriteLine("Received unknown UDP Header: {0}", messageHeader.Identity);
                        break;
                }
            }
        }

        private byte[] ParseAndDecode(BinaryReader reader, int count)
        {
            var encodedBuffer = reader.ReadBytes(count);

            return _codec.Decode(encodedBuffer);
        }

        private static void ProcessDescript(MessageHeader header, DescriptMessage descriptHeader, byte[] decodedBytes)
        {
            using (var memoryStream = new MemoryStream(decodedBytes))
            using (var reader = new BinaryReader(memoryStream))
            {
                switch (descriptHeader.Command)
                {
                    case UdpMessages.PeerAddr:

                        // 0x2100

                        break;

                    default:
                        Console.WriteLine("Unprocessed Descript {0}", descriptHeader);
                        break;
                }
            }
        }
    }
}
