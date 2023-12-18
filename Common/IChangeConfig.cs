using System.ServiceModel;

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
