using MasterChef.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            IMasterChefData data = new MasterChefData(new MasterChefDbContext());

            data.Categories.Add(new MasterChef.Models.Category()
            {
                Name = "Gosho",
                Picture = "www.GoshoPicture.com"
            });

            data.SaveChanges();
        }
    }
}
