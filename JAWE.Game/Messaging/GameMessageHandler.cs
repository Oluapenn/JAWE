using JAWE.Network.Messaging;
using JAWE.Network.Messaging.Processing;

namespace JAWE.Game.Messaging
{
    internal abstract class GameMessageHandler<TMessageType> : MessageHandler<GameClient, GameSessionFlags, TMessageType>
        where TMessageType : IMessage
    {

    }
}
