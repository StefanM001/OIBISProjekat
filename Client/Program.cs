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

            while (true)
            {
                Console.WriteLine("\n1: Procitaj konfiguraciju");
                Console.WriteLine("2: Dodaj proces");
                Console.WriteLine("3: Izmeni parametre konfiguracije");
                Console.WriteLine("4: Izbrisi proces");
                Console.WriteLine("5: Izbrisi konfiguracioni fajl");
                Console.WriteLine("6: Zavrsi rad programa");
                int izbor;

                do
                {
                    izbor = int.Parse(Console.ReadLine());
                } while (izbor < 1 || izbor > 6);

                if(izbor == 1)
                {
                    foreach(string s in proxy.ReadConfiguration())
                    {
                        Console.WriteLine(s);
                    }
                }
                else if(izbor == 2)
                {
                    Console.Write("Unesi proces: ");
                    string proces = Console.ReadLine();
                    Console.WriteLine(proxy.AddProcess(proces));
                }
                else if(izbor == 3)
                {
                    Console.Write("Uneti novi proces: ");
                    string p1 = Console.ReadLine();
                    Console.Write("Uneti proces za izmenu: ");
                    string p2 = Console.ReadLine();
                    Console.WriteLine(proxy.ModifyProcess(p1, p2));
                }
                else if(izbor == 4)
                {
                    Console.Write("Unesi proces: ");
                    string proces = Console.ReadLine();
                    Console.WriteLine(proxy.DeleteProcess(proces));
                }
                else if(izbor == 5)
                {
                    Console.WriteLine(proxy.DeleteConfigurationFile());
                }
                else
                {
                    break;
                }
            }

            Console.ReadKey();
        }
    }
}
