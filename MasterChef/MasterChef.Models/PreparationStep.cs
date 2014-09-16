namespace MasterChef.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PreparationStep
    {
        public int Id { get; set; }

        [Required]
        public int Minutes { get; set; }

        [Required]
        public int StepNumber { get; set; }

        [Required]
        [MinLength(6)]
        public string Text { get; set; }

        [Required]
        public object RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
