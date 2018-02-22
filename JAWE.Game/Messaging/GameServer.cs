using JAWE.Network;

namespace JAWE.Game.Messaging
{
    internal class GameServer : TcpServer<GameClient>
    {
        public GameServer()
            : base(5340, 10)
        {

        }
    }
}
