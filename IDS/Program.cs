using Common;
using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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
            EndpointAddress address = new EndpointAddress(new Uri("net.tcp://localhost:4000/IMalwareScanning"),
                                      new X509CertificateEndpointIdentity(srvCert));

            ChannelFactory<IMalwareScanning> channel = new ChannelFactory<IMalwareScanning>(binding, address);

            string cltCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            channel.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.ChainTrust;
            channel.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            /// Set appropriate client's certificate on the channel. Use CertManager class to obtain the certificate based on the "cltCertCN"
            channel.Credentials.ClientCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, cltCertCN);

            

            /// Use CertManager class to obtain the certificate based on the "srvCertCN" representing the expected service identity.


            IMalwareScanning proxy = channel.CreateChannel();

            Console.WriteLine(proxy.SendAlarmToIDS());

            proxy.SendAlarmToMS();

            Console.ReadKey();
        }
    }
}
