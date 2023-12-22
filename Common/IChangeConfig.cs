using System.Collections.Generic;
using System.ServiceModel;

namespace Common
{
    [ServiceContract]
    public interface IChangeConfig
    {
        [OperationContract]
        List<string> ReadConfiguration();

        [OperationContract]
        string AddProcess(string process);

        [OperationContract]
        string ModifyProcess(string p1, string p2);

        [OperationContract]
        string DeleteProcess(string process);

        [OperationContract]
        string DeleteConfigurationFile();
    }
}
