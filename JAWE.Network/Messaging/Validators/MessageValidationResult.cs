namespace JAWE.Network.Messaging.Validators
{
    public class MessageValidationResult
    {
        public MessageValidationResult(object message, int parametersTaken)
        {
            Message = message;
            ParametersTaken = parametersTaken;
        }

        public object Message { get; }
        public int ParametersTaken { get; }
    }
}