namespace MasterChef.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.Dependencies;
    using MasterChef.Data;
    using MasterChef.Web.Controllers;

    class TestBugsDependencyResolver : IDependencyResolver
    {
        private IMasterChefData data;

        public IMasterChefData Data
        {
            get
            {
                return this.data;
            }
            set
            {
                this.data = value;
            }
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            //add all controllers
            if (serviceType == typeof(RecipesController))
            {
                return new RecipesController(this.data, null, null, null);
            }
            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return new List<object>();
        }

        public void Dispose()
        {

        }
    }
}
