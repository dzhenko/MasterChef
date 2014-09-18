namespace MasterChef.Web.DataModels
{
    using System;
    using System.Linq;
    using MasterChef.Models;

    public class CategoryDataModel
    {
        public static Func<Category, CategoryDataModel> FromDataToModel
        {
            get
            {
                return c => new CategoryDataModel()
                {
                    Name = c.Name,
                    Picture = c.Picture
                };
            }
        }

        public string Name { get; set; }

        public string Picture { get; set; }
    }
}