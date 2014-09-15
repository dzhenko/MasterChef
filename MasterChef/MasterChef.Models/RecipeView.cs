namespace MasterChef.Models
{
    public class RecipeView
    {
        public int Id { get; set; }

        public bool? Liked { get; set; }

        public object UserId { get; set; }

        public virtual User User { get; set; }

        public object RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
