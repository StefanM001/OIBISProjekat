using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface IIDS
    {
        [OperationContract]
        void SendMessage(string message, byte[] sign);

        [OperationContract]
        string GetIntegrityReport();

        [OperationContract]
        void Send(string message);
    }
}
