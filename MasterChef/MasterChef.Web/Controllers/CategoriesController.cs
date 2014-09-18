namespace MasterChef.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using MasterChef.Data;
    using MasterChef.Models;
    using MasterChef.Web.DataModels;
    using MasterChef.Web.Infrastructure;
    using MasterChef.Web.Providers;

    [Authorize]
    [EnableCors("*", "*", "*")]
    public class CategoriesController : BaseApiController
    {
        public CategoriesController(IMasterChefData data, IUserIdProvider userIdProvider)
            : base(data, userIdProvider)
        {
        }

        public IHttpActionResult Get()
        {
            var categories = this.Data.Categories.All().Select(CategoryDataModel.FromDataToModel);
            return this.Ok(categories);
        }
    }
}