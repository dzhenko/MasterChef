namespace MasterChef.Service
{
    using System;
    using System.Linq;
    using System.ServiceModel;

    [ServiceContract]
    public interface IRecipeService
    {
        [OperationContract]
        string GiveChannelName(Guid id);

        [OperationContract]
        void StartCoocking();

        [OperationContract]
        string FinishedCooking();
    }
}
