using System;
using System.Collections.Generic;
using JAWE.Network.Messages.States;

namespace JAWE.Game.Lobby
{
    internal static class Channels
    {
        public static AiServerType AiConfig = AiServerType.Disabled;

        private static readonly Dictionary<byte, Channel> ChannelsList = new Dictionary<byte, Channel>();

        static Channels()
        {
            if (AiConfig != AiServerType.AiOnly)
            {
                AddChannel(ChannelType.CQC);
                AddChannel(ChannelType.UrbanOps);
                AddChannel(ChannelType.BattleGroup);
            }

            if (AiConfig != AiServerType.Disabled)
            {
                AddChannel(ChannelType.Ai);
            }

            Console.WriteLine("Initialized Channels({0})", string.Join(", ", ChannelsList.Values));
        }

        private static void AddChannel(ChannelType channelType)
        {
            var channel = new Channel(channelType);

            ChannelsList.Add((byte) channel.Id, channel);
        }

        public static Channel Get(ChannelType channelType)
        {
            var channelId = (byte) channelType;

            return ChannelsList.ContainsKey(channelId)
                ? ChannelsList[channelId]
                : null;
        }

        public static IEnumerable<Channel> All()
        {
            return ChannelsList.Values;
        }
    }
}