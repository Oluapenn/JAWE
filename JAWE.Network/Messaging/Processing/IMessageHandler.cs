namespace JAWE.Network.Messaging.Processing
{
    public interface IMessageHandler
    {
        bool Handle(Client abstractClient, IMessage message);
    }
}
