namespace JAWE.Test.Messaging.Parsing
{
    public interface IMessageParser<in TInput>
    {
        IMessage Parse(TInput input);
    }
}
