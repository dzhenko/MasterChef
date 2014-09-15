namespace MasterChef.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        private ICollection<Recipe> recipies;
        private ICollection<Comment> comments;
        private ICollection<RecipeView> recipeViews;

        public User()
            : base()
        {
            this.InitializeCollections();
        }

        public User(string userName)
            : base(userName)
        {
            this.InitializeCollections();
        }

        public virtual ICollection<Recipe> Recipies
        {
            get { return this.recipies; }
            set { this.recipies = value; }
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

        private void InitializeCollections()
        {
            this.recipies = new HashSet<Recipe>();
            this.comments = new HashSet<Comment>();
            this.recipeViews = new HashSet<RecipeView>();
        }
    }
}
