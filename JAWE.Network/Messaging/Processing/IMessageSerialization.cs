namespace JAWE.Network.Messaging.Processing
{
    public interface IMessageSerialization
    {
        void Deserialized();

        /// <summary>
        /// Return the number of parameters to take or -1 for all
        /// </summary>
        int BeforeSerialization();
    }
}