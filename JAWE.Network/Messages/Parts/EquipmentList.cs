using JAWE.Network.Messaging;
using JAWE.Network.Messaging.Attributes;

namespace JAWE.Network.Messages.Parts
{
    public class EquipmentList : IMessage
    {

        #region Parameters

        [Parameter(0)]
        public string Engineer { get; set; }

        [Parameter(1)]
        public string Medic { get; set; }

        [Parameter(2)]
        public string Sniper { get; set; }

        [Parameter(3)]
        public string Assault { get; set; }

        [Parameter(4)]
        public string Heavy { get; set; }

        #endregion

        #region Constructors

        public EquipmentList(string[] branches)
        {
            Engineer = branches[0];
            Medic = branches[1];
            Sniper = branches[2];
            Assault = branches[3];
            Heavy = branches[4];
        } 

        #endregion

    }
}
