using Common;
using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Channels;
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

            string cltCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name.ToLower());

            channel.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.ChainTrust;
            channel.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            /// Set appropriate client's certificate on the channel. Use CertManager class to obtain the certificate based on the "cltCertCN"
            channel.Credentials.ClientCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, cltCertCN);


            Console.WriteLine(WindowsIdentity.GetCurrent().Name);

            /// Use CertManager class to obtain the certificate based on the "srvCertCN" representing the expected service identity.


            IMalwareScanning proxy = channel.CreateChannel();

            X509Certificate2 certificateSign = CertManager.GetCertificateFromStorage(StoreName.My,
                   StoreLocation.LocalMachine, "wcfclient_sign");

            string message = "Testiranje slanja poruke digitalnim potpisom.";

            byte[] signature = DigitalSignature.Create(message, HashAlgorithm.SHA1, certificateSign);

            proxy.SendMessage(message, signature);
            Console.WriteLine("SendMessage() using {0} certificate finished. Press <enter> to continue ...", cltCertCN);


            string wrongCertCN = "wrong_sign";
            X509Certificate2 certificateSignWrong = CertManager.GetCertificateFromStorage(StoreName.My,
                       StoreLocation.LocalMachine, wrongCertCN);

            byte[] signatureWrong = DigitalSignature.Create(message, HashAlgorithm.SHA1, certificateSignWrong);

            proxy.SendMessage(message, signatureWrong);
            Console.WriteLine("SendMessage() using {0} certificate finished. Press <enter> to continue ...", wrongCertCN);



            Console.WriteLine(proxy.SendAlarmToIDS());

            proxy.SendAlarmToMS();

            Console.ReadKey();
        }
    }
}
