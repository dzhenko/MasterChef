namespace MasterChef.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class RecipeView
    {
        public int Id { get; set; }

        public bool? Liked { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        public Guid RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
