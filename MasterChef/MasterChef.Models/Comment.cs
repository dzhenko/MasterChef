namespace MasterChef.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [MinLength(12)]
        public string Text { get; set; }

        public object RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }

        public object UserId { get; set; }

        public virtual User User { get; set; }
    }
}
