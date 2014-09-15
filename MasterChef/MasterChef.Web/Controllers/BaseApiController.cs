namespace MasterChef.Web.Controllers
{
    using System.Web.Http;

    using MasterChef.Data;
    using MasterChef.Web.Infrastructure;
    
    public abstract class BaseApiController : ApiController
    {
        private IMasterChefData data;
        private IUserIdProvider userIdProvider;

        protected BaseApiController(IMasterChefData data, IUserIdProvider userIdProvider)
        {
            this.data = data;
            this.userIdProvider = userIdProvider;
        }

        protected IMasterChefData Data
        {
            get
            {
                return this.data;
            }
        }

        protected IUserIdProvider UserIdProvider
        {
            get
            {
                return this.userIdProvider;
            }
        }


    }
}