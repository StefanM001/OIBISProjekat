using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Principal;
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
            binding.Security.Mode = SecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType =
            TcpClientCredentialType.Windows;
            binding.Security.Transport.ProtectionLevel =
            ProtectionLevel.EncryptAndSign;

            ChannelFactory<IChangeConfig> channel = new ChannelFactory<IChangeConfig>(binding, address);

            IChangeConfig proxy = channel.CreateChannel();

            Console.WriteLine("Connection with MS servis is succesfull.");
            Console.WriteLine("Korisnik koji je pokrenuo klijenta je : " + WindowsIdentity.GetCurrent().Name);
            Console.WriteLine("//Login screen and implementation for changing autorization levels comes later.");

            Console.WriteLine(proxy.SendMessage());

            Console.ReadKey();
        }
    }
}
