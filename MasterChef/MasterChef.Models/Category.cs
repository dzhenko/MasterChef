namespace MasterChef.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        private ICollection<Recipe> recipies;

        public Category()
        {
            this.recipies = new HashSet<Recipe>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Picture { get; set; }

        public virtual ICollection<Recipe> Recipies
        {
            get { return this.recipies; }
            set { this.recipies = value; }
        }
    }
}
