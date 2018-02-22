using System;
using JAWE.Network.Messages.States;
using JAWE.Network.Messaging;
using JAWE.Network.Messaging.Attributes;
using JAWE.Network.Messaging.Processing;

namespace JAWE.Network.Messages
{
    [Message(MessageId.SerialGServ, Server = true)]
    public class SerialGServServer : BaseMessage, IMessageSerialization
    {

        #region Static

        private static readonly DateTime WrDateTime;

        static SerialGServServer()
        {
            WrDateTime = DateTime.UtcNow.AddYears(-1900).AddMonths(-1);
        }

        #endregion

        #region Parameters

        [Parameter(0)]
        public SerialGameStatusCode Status { get; set; }

        [Parameter(1, false)]
        public string DateString { get; set; }

        //[Parameter(2)]
        //public int ConnectionId { get; set; }

        #endregion

        #region Constructors

        public SerialGServServer(SerialGameStatusCode serialGameStatusCode)
        {
            Status = serialGameStatusCode;
        }

        public SerialGServServer()
        {
            Status = SerialGameStatusCode.Success;
            DateString = $"{WrDateTime:s/m/H/d/M/yyy}/{(int)WrDateTime.DayOfWeek}/{WrDateTime.DayOfYear}/{(WrDateTime.IsDaylightSavingTime() ? 0 : 1)}";
        }

        #endregion

        #region Serialization

        public void Deserialized()
        {
        }

        public int BeforeSerialization()
        {
            return Status != SerialGameStatusCode.Success ? 1 : -1;
        }

        #endregion

    }
}
