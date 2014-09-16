namespace MasterChef.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        public Guid RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
