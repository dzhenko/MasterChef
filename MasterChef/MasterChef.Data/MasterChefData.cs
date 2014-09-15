namespace MasterChef.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MasterChef.Data.Repositories;
    using MasterChef.Models;

    public class MasterChefData : IMasterChefData
    {
        private MasterChefDbContext context;
        private IDictionary<Type, object> repositories;

        public MasterChefData(MasterChefDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<Comment> Comments
        {
            get
            {
                return this.GetRepository<Comment>();
            }
        }

        public IRepository<PreparationStep> PreparationSteps
        {
            get
            {
                return this.GetRepository<PreparationStep>();
            }
        }

        public IRepository<Recipe> Recipies
        {
            get
            {
                return this.GetRepository<Recipe>();
            }
        }

        public IRepository<RecipeView> RecipeViews
        {
            get
            {
                return this.GetRepository<RecipeView>();
            }
        }

        public IRepository<User> Users
        {
            get
            {
                return this.GetRepository<User>();
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                return this.GetRepository<Category>();
            }
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);

            if (!this.repositories.ContainsKey(typeOfModel))
            {
                this.repositories.Add(typeOfModel, Activator.CreateInstance(typeof(Repository<T>), this.context));
            }

            return (IRepository<T>)this.repositories[typeOfModel];
        }
    }
}
