﻿namespace MasterChef.Web.DataModels
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using MasterChef.Models;
    using System.Collections.Generic;

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
                    Name = r.Name,
                    Id = r.Id.ToString()
                };
            }
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string Id { get; set; }
    }
}