namespace MasterChef.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;

    using Microsoft.AspNet.Identity.EntityFramework;

    using MasterChef.Data.Migrations;
    using MasterChef.Models;

    public class MasterChefDbContext : IdentityDbContext<User>
    {
        public MasterChefDbContext()
            : base("MasterChefConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer<MasterChefDbContext>(new MigrateDatabaseToLatestVersion<MasterChefDbContext, Configuration>());
        }
        public static MasterChefDbContext Create()
        {
            return new MasterChefDbContext();
        }

        public IDbSet<Recipe> Recipes { get; set; }

        // public IDbSet<User> Users { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<PreparationStep> PreparationSteps { get; set; }

        public IDbSet<RecipeView> RecipeViews { get; set; }

        public IDbSet<Category> Categories { get; set; }
    }
}
