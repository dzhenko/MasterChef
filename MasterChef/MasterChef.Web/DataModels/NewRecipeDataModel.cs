namespace MasterChef.Web.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    using System.Linq;

    using MasterChef.Models;

    public class NewRecipeDataModel
    {
        public static Recipe FromModelToData(NewRecipeDataModel data, Category category)
        {
            return new Recipe()
            {
                Name = data.Name,
                Image = data.Image,
                Description = data.Description,
                CategoryId = category.Id
            };
        }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        [Required]
        public string Category { get; set; }

        public ICollection<PreparationStepDataModel> PreparationSteps { get; set; }
    }
}