namespace MasterChef.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PreparationStep
    {
        public int Id { get; set; }

        public int Minutes { get; set; }

        [Required]
        [MinLength(12)]
        public string Text { get; set; }

        public object RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
