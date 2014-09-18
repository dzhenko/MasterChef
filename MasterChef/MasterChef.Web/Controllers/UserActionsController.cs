namespace MasterChef.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using MasterChef.Data;
    using MasterChef.Web.Infrastructure;

    [Authorize]
    [EnableCors("*", "*", "*")]
    public class UserActionsController : BaseApiController
    {
        public UserActionsController(IMasterChefData data, IUserIdProvider userIdProvider)
            : base(data, userIdProvider)
        {
        }

        [HttpPost]
        public IHttpActionResult View()
        {
            return this.Ok();
        }
    }
}