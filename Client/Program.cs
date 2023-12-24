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

            ClientMenu(proxy);

            Console.ReadKey();
        }

        static void ClientMenu(IChangeConfig proxy)
        {
            while (true)
            {
                Console.WriteLine("\n1: Procitaj konfiguraciju");
                Console.WriteLine("2: Dodaj proces");
                Console.WriteLine("3: Izmeni parametre konfiguracije");
                Console.WriteLine("4: Izbrisi proces");
                Console.WriteLine("5: Izbrisi konfiguracioni fajl");
                Console.WriteLine("6: Zavrsi rad programa");

                if (!Int32.TryParse(Console.ReadLine(), out int izbor) || izbor < 1 || izbor > 6)
                {
                    if (izbor == 6)
                    {
                        break;
                    }
                    Console.WriteLine("Niste izabrali opciju u korektnom formatu. Pokusajte ponovo.");
                    continue;
                }

                SwitchCaseForClientMenu(izbor, proxy);
            }
        }

        static void SwitchCaseForClientMenu(int izbor, IChangeConfig proxy)
        {
            switch (izbor)
            {
                case 1:
                    try
                    {
                        foreach (string s in proxy.ReadConfiguration())
                        {
                            Console.WriteLine(s);
                        }
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                    break;
                case 2:
                    try
                    {
                        Console.Write("Unesi proces: ");
                        string proces = Console.ReadLine();
                        Console.WriteLine(proxy.AddProcess(proces));
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                    break;
                case 3:
                    try
                    {
                        Console.Write("Uneti novi proces: ");
                        string p1 = Console.ReadLine();
                        Console.Write("Uneti trenutni proces koji zelite izmeniti: ");
                        string p2 = Console.ReadLine();
                        Console.WriteLine(proxy.ModifyProcess(p1, p2));
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                    break;
                case 4:
                    try
                    {
                        Console.Write("Unesi proces za brisanje: ");
                        string proces = Console.ReadLine();
                        Console.WriteLine(proxy.DeleteProcess(proces));
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                    break;
                case 5:
                    try
                    {
                        Console.WriteLine(proxy.DeleteConfigurationFile());
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                    break;
                case 6:
                    Console.WriteLine("Working is done. Press any key to exit...");
                    Console.ReadKey();
                    return;
                    break;
            }
        }
    }
}
