namespace MasterChef.Web.DataModels
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using MasterChef.Models;
    using System.Collections.Generic;

    public class RecipeExtendedOverviewDataModel
    {
        public static Expression<Func<Recipe, RecipeExtendedOverviewDataModel>> FromDataToModelExtended(string userId)
        {
            return r => new RecipeExtendedOverviewDataModel()
            {
                Description = r.Description,
                Image = r.Image,
                Name = r.Name,
                Id = r.Id.ToString(),
                LikedByUser = r.RecipeViews.FirstOrDefault(rv => rv.UserId == userId).Liked
            };
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string Id { get; set; }

        public bool? LikedByUser { get; set; }
    }
}