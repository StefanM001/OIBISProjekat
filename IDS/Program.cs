using Common;
using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Formatter = Manager.Formatter;

namespace IDS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            string srvCertCN = "wcfservice";

            X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, srvCertCN);
            EndpointAddress address = new EndpointAddress(new Uri("net.tcp://localhost:4001/IMalwareScanning"),
                                      new X509CertificateEndpointIdentity(srvCert));

            ChannelFactory<IMalwareScanning> channel = new ChannelFactory<IMalwareScanning>(binding, address);

            string cltCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            channel.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.ChainTrust;
            channel.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;
            channel.Credentials.ClientCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, cltCertCN.ToLower());

            Console.WriteLine(WindowsIdentity.GetCurrent().Name);

            try
            {
                IMalwareScanning proxy = channel.CreateChannel();

                Console.WriteLine(proxy.SendAlarmToIDS());

                proxy.SendAlarmToMS();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error detected: " + ex.Message);
            }

            Console.ReadKey();
        }
    }
}
