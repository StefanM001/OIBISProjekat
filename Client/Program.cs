﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string address = "net.tcp://localhost:4000/IMalwareScanning";
            NetTcpBinding binding = new NetTcpBinding();

            ChannelFactory<IChangeConfig> channel = new ChannelFactory<IMalwareScanning>(binding, address);

            IMalwareScanning proxy = channel.CreateChannel();

            Console.WriteLine(proxy.SendAlarmToIDS());

            Console.ReadKey();
        }
    }
    }
}
