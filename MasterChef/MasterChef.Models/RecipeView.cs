namespace MasterChef.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RecipeView
    {
        public int Id { get; set; }

        public bool? Liked { get; set; }

        [Required]
        public object UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        public object RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
