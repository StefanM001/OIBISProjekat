using Common;
using System;
using System.Net.Security;
using System.Security.Principal;
using System.ServiceModel;


namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string address = "net.tcp://localhost:4000/IChangeConfig";
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

            try
            {
                proxy.AddProcess();
                Console.WriteLine("Add process allowed");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to add process : {0}", e.Message);
            }

            try
            {
                proxy.ReadConfiguration();
                Console.WriteLine("Read configuration allowed");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to read configuration : {0}", e.Message);
            }

            try
            {
                proxy.DeleteConfigurationFile();
                Console.WriteLine("Delete configuration file allowed");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to delete configuration file : {0}", e.Message);
            }

            try
            {
                proxy.DeleteProcess();
                Console.WriteLine("Delete process allowed");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to delete process : {0}", e.Message);
            }

            try
            {
                proxy.ModifyProcess();
                Console.WriteLine("Modify process allowed");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to modify process : {0}", e.Message);
            }

            Console.ReadKey();
        }
    }
}
