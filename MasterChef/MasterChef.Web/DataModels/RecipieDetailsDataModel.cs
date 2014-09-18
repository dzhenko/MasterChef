namespace MasterChef.Web.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using MasterChef.Models;

    public class RecipieDetailsDataModel
    {
        public static Func<Recipe, RecipieDetailsDataModel> FromDataToModel
        {
            get
            {
                return r => new RecipieDetailsDataModel()
                {
                    Category = r.Category.Name,
                    Comments = r.Comments.Select(CommentDataModel.FromDataToModel),
                    Description = r.Description,
                    Image = r.Image,
                    Name = r.Name,
                    Products = r.Products.Split(';'),
                    PreparationSteps = r.PreparationSteps.Select(PreparationStepDataModel.FromDataToModel),
                    RecipeViews = r.RecipeViews.Count(),
                    RecipeLikes = r.RecipeViews.Count(l => l.Liked == true),
                    User = r.User.UserName
                };
            }
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<string> Products { get; set; }

        public string Image { get; set; }

        public string User { get; set; }

        public string Category { get; set; }

        public IEnumerable<PreparationStepDataModel> PreparationSteps { get; set; }

        public IEnumerable<CommentDataModel> Comments {get; set;}

        public int RecipeViews { get; set; }

        public int RecipeLikes { get; set; }
    }
}