﻿using System;

namespace JAWE.Network
{
    public class DataReceivedEventArgs : EventArgs
    {
        public byte[] Data { get; }

        public DataReceivedEventArgs(byte[] data)
        {
            Data = data;
        }
    }
}