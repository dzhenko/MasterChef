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
        public static Recipe FromModelToData(NewRecipeDataModel data, int categoryId)
        {
            return new Recipe()
            {
                Name = data.Name,
                Description = data.Description,
                CategoryId = categoryId,
                Products = string.Join(";", data.Products)
            };
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Image { get; set; }

        public IEnumerable<string> Products { get; set; }

        [Required]
        public string Category { get; set; }

        public ICollection<PreparationStepDataModel> PreparationSteps { get; set; }
    }
}