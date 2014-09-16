namespace MasterChef.Web.DataModels
{
    using MasterChef.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

    public class PreparationStepDataModel
    {
        public static Func<PreparationStepDataModel, PreparationStep> FromModelToData
        {
            get
            {
                return (p => new PreparationStep()
                {
                    Minutes = p.Minutes,
                    Text = p.Text,
                    StepNumber = p.StepNumber
                });
            }
        }

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

        [Required]
        public int Minutes { get; set; }

        [Required]
        public int StepNumber { get; set; }

        [Required]
        [MinLength(6)]
        public string Text { get; set; }
    }
}
