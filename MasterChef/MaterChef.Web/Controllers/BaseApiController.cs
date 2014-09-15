namespace MaterChef.Web.Controllers
{
    using MasterChef.Data;
    using MaterChef.Web.Infrastructure;
    using System.Web.Http;
    
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