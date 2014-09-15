namespace MasterChef.Web.DataModels
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using MasterChef.Models;

    public class RecipeOverviewDataModel
    {
        public static Expression<Func<Recipe, RecipeOverviewDataModel>> FromDataToModel
        {
            get
            {
                return r => new RecipeOverviewDataModel() 
                {
                    Description = r.Description,
                    Image = r.Image,
                    Name = r.Name
                };
            }
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }
    }
}