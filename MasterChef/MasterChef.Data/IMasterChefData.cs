namespace MasterChef.Data
{
    using System;
    using System.Linq;

    using MasterChef.Data.Repositories;
    using MasterChef.Models;

    public interface IMasterChefData
    {
        IRepository<Comment> Comments { get; }

        IRepository<PreparationStep> PreparationSteps { get; }

        IRepository<Recipe> Recipies { get; }

        IRepository<RecipeView> RecipeViews { get; }

        IRepository<User> Users { get; }

        IRepository<Category> Categories { get; }

        void SaveChanges();
    }
}
