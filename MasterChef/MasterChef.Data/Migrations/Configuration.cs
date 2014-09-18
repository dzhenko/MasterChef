namespace MasterChef.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using MasterChef.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MasterChefDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MasterChefDbContext context)
        {
            this.AddCategories(context);

            context.SaveChanges();
        }

        private void AddCategories(MasterChefDbContext context)
        {
            if (context.Categories.Any())
            {
                return;
            }
            context.Categories.Add(new Category()
            {
                Name = "Soup",
                Picture = "http://www.youngandraw.com/wp-content/uploads/Apple-Cumin-Green-Soup.jpg",
            });
            
            context.Categories.Add(new Category()
            {
                Name = "Salad",
                Picture = "http://flipcookbook.zippykid.netdna-cdn.com/wp-content/uploads/2010/09/beetsalad-final.jpg",
            });

            context.Categories.Add(new Category()
            {
                Name = "Main Dish",
                Picture = "http://www.p3crossfit.com/wp-content/uploads/2013/12/HoneyWasabiSalmon.jpg",
            });

            context.Categories.Add(new Category()
            {
                Name = "Dessert",
                Picture = "http://www.dessertrecept.nl/wp-content/uploads/2013/06/chocotaart.jpg",
            });
        }
    }
}
