namespace JAWE.Network.Messaging.Validators
{
    public class ParameterValidationResult
    {
        public ParameterValidationResult(ParameterValidationStatus status, int parametersTaken = 1)
        {
            Status = status;
            ParametersTaken = parametersTaken;
        }

        public ParameterValidationStatus Status { get; private set; }
        public int ParametersTaken { get; private set; }
        public object State { get; set; }
    }
}