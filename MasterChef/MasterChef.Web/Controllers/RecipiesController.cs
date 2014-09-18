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
    public class RecipesController : BaseApiController
    {
        private readonly IImageUploadProvider imageUploadProvider;
        private readonly INotificationProvider notificationProvider;

        public RecipesController(IMasterChefData data, IUserIdProvider userIdProvider, 
            IImageUploadProvider imageUploadProvider, INotificationProvider notificationProvider)
            : base(data, userIdProvider)
        {
            this.imageUploadProvider = imageUploadProvider;
            this.notificationProvider = notificationProvider;
        }

        public IHttpActionResult Post([FromBody] NewRecipeDataModel newRecipeDataModel)
        {
            if (newRecipeDataModel == null || !ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var category = this.Data.Categories.All().FirstOrDefault(c => c.Name == newRecipeDataModel.Category);

            if (category == null)
            {
                return this.BadRequest("You can not use unexisting categories");
            }

            var recipe = NewRecipeDataModel.FromModelToData(newRecipeDataModel, category.Id);

            recipe.UserId = this.UserIdProvider.GetUserId();

            var username = this.Data.Users.Find(recipe.UserId).UserName;

            this.Data.Recipies.Add(recipe);
            
            if (newRecipeDataModel.PreparationSteps != null && newRecipeDataModel.PreparationSteps.Count > 0)
            {
                recipe.PreparationSteps = new HashSet<PreparationStep>
                    (newRecipeDataModel.PreparationSteps.Select(r => PreparationStepDataModel.FromModelToData(r, recipe.Id)));
            }

            recipe.Image = this.imageUploadProvider.UploadImage(newRecipeDataModel.Image, recipe.Id.ToString());

            this.Data.SaveChanges();

            notificationProvider.Notify(string.Format("{0}<<<{1} added new recipe: {2}", recipe.Id, username, recipe.Name));

            return this.Ok(recipe.Id);
        }

        public IHttpActionResult Get()
        {
            return this.Ok(this.Data.Recipies.All().Select(RecipeOverviewDataModel.FromDataToModel));
        }

        public IHttpActionResult Get(string id)
        {
            var recipe = this.GetRecipeById(id);

            if (recipe == null)
            {
                return this.BadRequest("Invalid Id - recipie not found");
            }

            var model = RecipieDetailsDataModel.FromDataToModel(recipe);

            var liked = this.ViewRecipe(model.Id);

            model.Liked = liked;

            return Ok(model);
        }

        public IHttpActionResult Get(string param, string value)
        {
            if (string.IsNullOrEmpty(param) || string.IsNullOrWhiteSpace(param))
            {
                return this.BadRequest("Invalid param - empty");
            }

            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value) || value == "undefined")
            {
                value = this.UserIdProvider.GetUserId();
            }

            var recipes = this.Data.Recipies.All();

            switch (param)
	        {
                case "name":recipes = recipes.Where(r => r.Name.ToLower().Contains(value.ToLower())); break;
                case "category":recipes = recipes.Where(r => r.Category.Name.ToLower() == value.ToLower()); break;
                case "user": recipes = recipes.Where(r => r.User.Id == value); break;
		        default: return this.BadRequest("Invalid param");
	        }

            return this.Ok(recipes.Select(RecipeOverviewDataModel.FromDataToModel));
        }

        public IHttpActionResult Delete(string id)
        {
            var recipe = GetRecipeById(id);

            if (recipe == null)
            {
                return this.BadRequest("Invalid id");
            }

            if (recipe.UserId != this.UserIdProvider.GetUserId())
            {
                return this.BadRequest("You are not the owner of this recipe");
            }

            this.Data.Recipies.Delete(recipe);
            this.Data.SaveChanges();

            return this.Ok(recipe.Id);
        }

        public IHttpActionResult Put(string id, string comment)
        {
            if (string.IsNullOrEmpty(comment))
            {
                return this.BadRequest("Invalid comment - null");
            }

            var recipe = this.GetRecipeById(id);

            if (recipe == null)
            {
                return this.BadRequest("Invalid Id");
            }

            var user = this.Data.Users.Find(this.UserIdProvider.GetUserId());

            this.Data.Comments.Add(CommentDataModel.FromModelToData(id, comment, user.Id));

            this.Data.SaveChanges();

            notificationProvider.Notify(string.Format("{0}<<<{1} commented recipe {2} : {3}",
                recipe.Id, user.UserName, recipe.Name, comment));

            return this.Ok();
        }

        public IHttpActionResult Put(string id)
        {
            var recipe = this.GetRecipeById(id);

            if (recipe == null)
            {
                return this.BadRequest("Recipe not found");
            }

            var user = this.Data.Users.Find(this.UserIdProvider.GetUserId());

            var view = user.RecipeViews.FirstOrDefault(rv => rv.RecipeId.ToString() == id);

            if (view.Liked == null)
            {
                view.Liked = true;
            }
            else
            {
                view.Liked = !view.Liked;
            }

            this.Data.RecipeViews.Update(view);

            this.Data.SaveChanges();

            notificationProvider.Notify(string.Format("{0}<<<{1} {2} recipe {3}", 
                recipe.Id, user.UserName, view.Liked == true ? "liked" : "disliked", recipe.Name));

            return this.Ok();
        }

        private bool? ViewRecipe(string recipeId)
        {
            var guidId = Guid.Parse(recipeId);

            var userId = this.UserIdProvider.GetUserId();
            var userView = this.Data.Users.Find(userId).RecipeViews.FirstOrDefault((rv => rv.RecipeId == guidId));

            bool? liked = null;

            if (userView == null)
            {
                this.Data.RecipeViews.Add(new RecipeView()
                {
                    UserId = userId,
                    RecipeId = guidId
                });

                this.Data.SaveChanges();
            }
            else
            {
                liked = userView.Liked;
            }

            return liked;
        }

        private Recipe GetRecipeById(string id)
        {
            Guid guidId;

            if (!Guid.TryParse(id, out guidId))
            {
                return null;
            }

            return this.Data.Recipies.All().Where(r => r.Id == guidId).FirstOrDefault();
        }
    }
}