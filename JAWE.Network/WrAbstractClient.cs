using JAWE.Network.Codec;
using System;
using System.Text;
using JAWE.Network.Messages;
using JAWE.Network.Messaging.Processing;

namespace JAWE.Network
{
    public abstract class WrAbstractClient<TSessionFlags> : SessionClient<TSessionFlags> where TSessionFlags : struct, IConvertible
    {
        private static readonly MessageProcessor MessageProcessor = new MessageProcessor();
        private static readonly Encoding StringEncoding = Encoding.UTF8;
        private static readonly uint MaxMessageSize = 0x4000;
        private static readonly char[] MessageDelimiter = { '\n' };

        // TODO move the encoder to a wrserver class.
        private readonly WrMessageCodec _encoder;

        private string _overflowBuffer;

        protected WrAbstractClient(WrMessageCodec encoder)
        {
            _overflowBuffer = string.Empty;
            _encoder = encoder;
            DataReceived += OnDataReceived;
            Initialized += OnInitialized;
        }

        private void OnDataReceived(object sender, DataReceivedEventArgs e)
        {
            var client = sender as Client;

            // Check the buffer stream after decoding.
            if (_overflowBuffer.Length + e.Data.Length > MaxMessageSize)
            {
                // We will not even bother to decode the next bytes since we assume it's garbage.
                client?.Disconnect("MaxMessageSize reached.");
                return;
            }

            // Decode the message bytes.
            var decoded = _encoder.Decode(e.Data);

            // Parse to string
            var receivedBuffer = StringEncoding.GetString(decoded);
            var messageBuffer = _overflowBuffer + receivedBuffer;

            // NOTE:    Normally you should disconnect people who send 'empty' packets but the developers messed up some where so the client might send \n\n twice.
            //          This does not apply to all client versions so make sure you check your version.
            var messages = messageBuffer.Split(MessageDelimiter, StringSplitOptions.RemoveEmptyEntries);

            foreach (var message in messages)
            {
                // Parse message.
                var parseMessage = message.Trim();

                try
                {
                    var wrMessage = MessageProcessor.Parse(parseMessage) as BaseMessage;
                    if (wrMessage != null)
                    {
                        MessageReceived.Invoke(this, new WrMessageReceivedEventArgs(wrMessage));
                    }
                    else
                    {
                        Console.WriteLine("Failed to parse message: {0}", parseMessage);
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Parse Exception: {0}", exception);
                }
            }

            _overflowBuffer = string.Empty;

            if (!messageBuffer.EndsWith("\n"))
            {
                // The last message is split, remember this for the next receive call.
                _overflowBuffer = messages[messages.Length - 1];
            }
        }

        public void Send(BaseMessage message)
        {
            if (message == null)
                throw new Exception("Send error, message = null");

            byte[] buffer;

            try
            {
                buffer = MessageToBytes(message);
            }
            catch (Exception)
            {
                Disconnect("An error occured while converting the message to bytes.");
                return;
            }

            Send(buffer);
        }

        /// <summary>
        /// Converts the BaseMessage to bytes and encodes them with xor.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private byte[] MessageToBytes(BaseMessage message)
        {
            var messageString = MessageProcessor.Build(message);
            var messageBytes = StringEncoding.GetBytes(messageString);

            Console.WriteLine("Sending >> {0}", messageString.Remove(messageString.Length - 1));

            return _encoder.Encode(messageBytes);
        }

        private void OnInitialized(object sender, EventArgs eventArgs)
        {
            Initialized -= OnInitialized;

            Console.WriteLine("AbstractClient Initialized, sending Hello.");

            // LaunchInfoServer
            var initializeConnection = new InitializeConnectionMessageServer
            {
                CrcSeed = 0,
                Version = 77,
            };

            Send(initializeConnection);
        }

        #region Events

        public EventHandler<WrMessageReceivedEventArgs> MessageReceived;

        #endregion
    }
}