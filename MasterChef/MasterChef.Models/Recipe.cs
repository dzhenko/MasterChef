namespace MasterChef.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Recipe
    {
        private ICollection<PreparationStep> preparationSteps;
        private ICollection<Comment> comments;
        private ICollection<RecipeView> recipeViews;
        
        public Recipe()
        {
            this.Id = Guid.NewGuid();

            this.preparationSteps = new HashSet<PreparationStep>();
            this.comments = new HashSet<Comment>();
            this.recipeViews = new HashSet<RecipeView>();
        }

        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<PreparationStep> PreparationSteps
        {
            get { return this.preparationSteps; }
            set { this.preparationSteps = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public virtual ICollection<RecipeView> RecipeViews
        {
            get { return this.recipeViews; }
            set { this.recipeViews = value; }
        }
    }
}
