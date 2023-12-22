using Common;
using Manager;
using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using Formatter = Manager.Formatter;

namespace IDS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using(ServiceHost host = new ServiceHost(typeof(IDSService)))
            {
                string address = "net.tcp://localhost:4001/IIDS";

                NetTcpBinding binding = new NetTcpBinding();
                binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;


                host.AddServiceEndpoint(typeof(IIDS), binding, address);

                host.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
                host.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });

                string srvCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

                host.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.ChainTrust;

                ///If CA doesn't have a CRL associated, WCF blocks every client because it cannot be validated
                host.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

                ///Set appropriate service's certificate on the host. Use CertManager class to obtain the certificate based on the "srvCertCN"
                host.Credentials.ServiceCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, "wcfservice");
                ServiceSecurityAuditBehavior newAudit = new ServiceSecurityAuditBehavior();
                newAudit.AuditLogLocation = AuditLogLocation.Application;
                newAudit.ServiceAuthorizationAuditLevel = AuditLevel.SuccessOrFailure;

                host.Description.Behaviors.Remove<ServiceSecurityAuditBehavior>();
                host.Description.Behaviors.Add(newAudit);

                host.Open();

                IDSLog iDSLog = new IDSLog();

                Console.WriteLine("Servis je uspesno pokrenut na adresi " + address);
                Console.ReadKey();
            }
        }
    }
}
