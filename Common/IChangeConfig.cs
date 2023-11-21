using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface IChangeConfig
    {
        [OperationContract]
        void ReadConfiguration();

        [OperationContract]
        void AddProcess();

        [OperationContract]
        void ModifyProcess();

        [OperationContract]
        void DeleteProcess();

        [OperationContract]
        void DeleteConfigurationFile();
    }
}
