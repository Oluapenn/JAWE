namespace JAWE.Test.Messaging.Parsing
{
    public class WrStrMessageParser : IMessageParser<string>
    {
        //private static readonly MessageValidator AbstractMessageValidator;

        //private readonly MessageProcessor _messageProcessor;

        static WrStrMessageParser()
        {
            //AbstractMessageValidator = new MessageValidator(typeof(BaseMessage));
        }

        /*public WrStrMessageParser(MessageProcessor messageProcessor)
        {
            //_messageProcessor = messageProcessor;
        }*/

        public IMessage Parse(string input)
        {
            // Validate minimum amount of parameters.
            var parameters = input.Split(' ');
            if (parameters.Length < 2)
            {
                throw new MessageParseException("Not enough parameters.");
            }

            // Validate Base message
            //var abstractMessage = AbstractMessageValidator.Validate(_messageProcessor, parameters).Message as BaseMessage;

            return null;
        }
    }
}
