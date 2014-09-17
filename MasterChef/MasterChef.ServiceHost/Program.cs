namespace MasterChef.ServiceHost
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.ServiceModel;

    using MasterChef.Service;
    using System.ServiceModel.Description;

    class Program
    {
        static void Main(string[] args)
        {
            var serviceAddress = new Uri("http://localhost:1234");
            var selfHost = new ServiceHost(typeof(RecipeService), serviceAddress);

            var smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            selfHost.Description.Behaviors.Add(smb);

            selfHost.Open();
            Console.WriteLine("Service started on {0} ", serviceAddress);
            Console.WriteLine("Press Enter to exit!");
            Console.ReadLine();;
        }
    }
}
