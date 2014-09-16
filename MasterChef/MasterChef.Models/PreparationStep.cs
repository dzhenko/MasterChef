namespace MasterChef.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PreparationStep
    {
        public int Id { get; set; }

        [Required]
        public int Minutes { get; set; }

        [Required]
        public int StepNumber { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public Guid RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
