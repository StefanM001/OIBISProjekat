using Common;
using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace IDS
{
    public class IDSService : IIDS
    {
        private static IDSLog iDSLog = new IDSLog();

        public void SendMessage(string message, byte[] sign)
        {
            //kad je u pitanju autentifikacija putem Sertifikata
            string clientName = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);

            string clientNameSign = clientName + "_sign";
            X509Certificate2 certificate = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople,
                StoreLocation.LocalMachine, clientNameSign);

            /// Verify signature using SHA1 hash algorithm
            if (DigitalSignature.Verify(message, HashAlgorithm.SHA1, sign, certificate))
            {
                iDSLog.LogAlarm(message);
            }
            else
            {
                Console.WriteLine("Sign is invalid");
            }
        }

        public string GetIntegrityReport()
        {
            return iDSLog.GetReports();
        }

        public void Send(string message)
        {
            iDSLog.LogAlarm(message);
        }
    }
}
