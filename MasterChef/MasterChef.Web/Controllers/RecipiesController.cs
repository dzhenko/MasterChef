namespace MasterChef.Web.Controllers
{
    using System;
    using System.Linq;

    using System.Web.Http;

    using MasterChef.Data;
    using MasterChef.Web.Infrastructure;
    using MasterChef.Web.DataModels;
    using MasterChef.Models;
    using System.Collections.Generic;

    [Authorize]
    public class RecipiesController : BaseApiController
    {
        public RecipiesController(IMasterChefData data, IUserIdProvider userIdProvider)
            : base(data, userIdProvider)
        {
        }

        [HttpPost]
        public IHttpActionResult Create([FromBody] NewRecipeDataModel newRecipeDataModel)
        {
            if (newRecipeDataModel == null || !ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var category = this.Data.Categories.All().FirstOrDefault(c => c.Name == newRecipeDataModel.Category);

            if (category == null)
            {
                // TODO: Extract all strings into resource
                return this.BadRequest("You can not use unexisting categories");
            }

            var recipe = NewRecipeDataModel.FromModelToData(newRecipeDataModel, category);

            if (newRecipeDataModel.PreparationSteps != null && newRecipeDataModel.PreparationSteps.Count > 0)
            {
                recipe.PreparationSteps = new HashSet<PreparationStep>
                    (newRecipeDataModel.PreparationSteps.Select(PreparationStepDataModel.FromModelToData));
            }

            recipe.UserId = this.UserIdProvider.GetUserId();

            this.Data.Recipies.Add(recipe);
            this.Data.SaveChanges();

            return this.Ok(recipe.Id);
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            return this.Ok(this.Data.Recipies.All().Select(RecipeOverviewDataModel.FromDataToModel));
        }
    }
}