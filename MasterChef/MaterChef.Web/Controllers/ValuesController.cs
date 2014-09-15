namespace MaterChef.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using MasterChef.Data;
    using MaterChef.Web.Infrastructure;

    [Authorize]
    public class ValuesController : BaseApiController
    {
        public ValuesController(
            IMasterChefData data,
            IUserIdProvider userIdProvider)
            : base(data, userIdProvider)
        {
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            this.Data.Recipies.All().Any();
            return new string[] { "magic", "damn" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
