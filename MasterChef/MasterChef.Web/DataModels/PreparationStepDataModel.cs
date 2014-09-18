namespace MasterChef.Web.DataModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MasterChef.Models;

    public class PreparationStepDataModel
    {
        public static Func<PreparationStep, PreparationStepDataModel> FromDataToModel
        {
            get
            {
                return (p => new PreparationStepDataModel()
                {
                    Minutes = p.Minutes,
                    Text = p.Text,
                    StepNumber = p.StepNumber
                });
            }
        }

        public static PreparationStep FromModelToData(PreparationStepDataModel model, Guid recipeId)
        {
            return new PreparationStep()
            {
                Minutes = model.Minutes,
                Text = model.Text,
                StepNumber = model.StepNumber,
                RecipeId = recipeId
            };
        }

        [Required]
        public int Minutes { get; set; }

        [Required]
        public int StepNumber { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
