using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using MasterChef.Data;
using MasterChef.Web.Infrastructure;
using Microsoft.Owin;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;

[assembly: OwinStartup(typeof(MasterChef.Web.Startup))]

namespace MasterChef.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(GlobalConfiguration.Configuration);
        }
        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            RegisterMappings(kernel);
            return kernel;
        }

        private static void RegisterMappings(StandardKernel kernel)
        {
            kernel.Bind<IMasterChefData>().To<MasterChefData>()
                .WithConstructorArgument("context",
                    c => new MasterChefDbContext());

            kernel.Bind<IUserIdProvider>().To<AspNetUserIdProvider>();
        }
    }
}
