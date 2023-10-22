using Common;
using System;
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
            string address = "net.tcp://localhost:4001/IChangeConfig";
            NetTcpBinding binding = new NetTcpBinding();

            ChannelFactory<IChangeConfig> channel = new ChannelFactory<IChangeConfig>(binding, address);

            IChangeConfig proxy = channel.CreateChannel();

            Console.WriteLine("Connection with MS servis is succesfull.");
            Console.WriteLine("//Login screen and implementation for changing autorization levels comes later.");

            Console.WriteLine(proxy.SendMessage());

            Console.ReadKey();
        }
    }
}
