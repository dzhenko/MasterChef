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
        private readonly IDropboxImageUploader imageUploadProvider;

        public RecipesController(IMasterChefData data, IUserIdProvider userIdProvider, IDropboxImageUploader imageUploadProvider)
            : base(data, userIdProvider)
        {
            this.imageUploadProvider = imageUploadProvider;
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

            var recipe = NewRecipeDataModel.FromModelToData(newRecipeDataModel, category.Id);

            recipe.UserId = this.UserIdProvider.GetUserId();

            this.Data.Recipies.Add(recipe);
            
            if (newRecipeDataModel.PreparationSteps != null && newRecipeDataModel.PreparationSteps.Count > 0)
            {
                recipe.PreparationSteps = new HashSet<PreparationStep>
                    (newRecipeDataModel.PreparationSteps.Select(r => PreparationStepDataModel.FromModelToData(r, recipe.Id)));
            }

            recipe.Image = this.imageUploadProvider.UploadImageToDropbox(newRecipeDataModel.Image, recipe.Id.ToString());
            
            this.Data.SaveChanges();

            return this.Ok(recipe.Id);
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            return this.Ok(this.Data.Recipies.All().Select(RecipeOverviewDataModel.FromDataToModel));
        }

        [HttpGet]
        public IHttpActionResult GetById(string id)
        {
            Guid guidId;

            if (!Guid.TryParse(id, out guidId))
            {
                return this.BadRequest("Invalid Id - invalid guid format");
            }

            var recipie = this.Data.Recipies.Find(guidId);

            if (recipie == null)
            {
                return this.BadRequest("Invalid Id - recipie not found");
            }

            return Ok(RecipieDetailsDataModel.FromDataToModel(recipie));
        }

        [HttpGet]
        public IHttpActionResult GetByName(string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                return this.BadRequest("Invalid name - empty");
            }

            return this.Ok(this.Data.Recipies.All().Where(r => r.Name.ToLower().Contains(name.ToLower())).Select(RecipeOverviewDataModel.FromDataToModel));
        }

        [HttpGet]
        public IHttpActionResult GenerateSampleData(string password)
        {
            if (password != "qwertyok")
            {
                return this.BadRequest("Invalid password");
            }

            try
            {
                this.GenerateSampleData();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }

            return this.Ok();
        }

        private void GenerateSampleData()
        {
            var categories = this.Data.Categories.All().Select(c => c.Id).ToArray();

            var user = this.Data.Users.All().FirstOrDefault();

            if (user == null)
            {
                throw new ArgumentNullException("No users in database");
            }

            var recipies = new List<Recipe>()
            {
                // soups
                new Recipe()
                {
                    CategoryId = categories[0],
                    Description = "Homemade Chicken soup",
                    Image = "http://www.taste.com.au/images/recipes/agt/2005/07/2458_l.jpg",
                    Name = "Chicken soup",
                    UserId = user.Id
                },
                new Recipe()
                {
                    CategoryId = categories[0],
                    Description = "Tomatoe soup",
                    Image = "http://www.healtheelife.com/wp-content/uploads/2014/06/tomato-soup1.jpg",
                    Name = "Tomatoe soup",
                    UserId = user.Id
                },
                // salads
                new Recipe()
                {
                    CategoryId = categories[1],
                    Description = "My favorite Carrot salad",
                    Image = "http://ecosalon.com/data/fe/image/carrot%20salad.jpg",
                    Name = "Carrot salad",
                    UserId = user.Id
                },
                new Recipe()
                {
                    CategoryId = categories[1],
                    Description = "Fresh Fruit salad",
                    Image = "http://media1.onsugar.com/files/2012/08/33/1/192/1922195/e20a68aa18dbc9cc_shutterstock_87164776.xxxlarge/i/How-Make-Really-Good-Fruit-Salad.jpg",
                    Name = "Fruit salad",
                    UserId = user.Id
                },
                new Recipe()
                {
                    CategoryId = categories[1],
                    Description = "Classical Shopska salad",
                    Image = "http://www.pizzadonvito.com/files/data_0/52/Image/eaUw-e02.jpg",
                    Name = "Shopska salad",
                    UserId = user.Id
                },
                // main dishes
                new Recipe()
                {
                    CategoryId = categories[2],
                    Description = "The besh grilled chicken ever for realz",
                    Image = "http://iamchampionaire.files.wordpress.com/2013/12/cooked-chicken.jpg",
                    Name = "Grilled chicken",
                    UserId = user.Id
                },
                new Recipe()
                {
                    CategoryId = categories[2],
                    Description = "Fresh Salomon with lemon",
                    Image = "http://dietfooddelivery.biz/wp-content/uploads/2014/06/cooked-fish-images.jpg",
                    Name = "Grilled salomon",
                    UserId = user.Id
                },
                new Recipe()
                {
                    CategoryId = categories[2],
                    Description = "Homemade pork steak with rice",
                    Image = "http://lowcarbgrub.com/wp-content/uploads/2012/10/steak_on_plate.jpg",
                    Name = "Pork steak",
                    UserId = user.Id
                },
                new Recipe()
                {
                    CategoryId = categories[2],
                    Description = "High carbon spaghetti bolognese",
                    Image = "http://img.food.com/img/recipes/14/35/19/large/picNlHUhE.jpg",
                    Name = "Spaghetti bolognese",
                    UserId = user.Id
                },
                new Recipe()
                {
                    CategoryId = categories[2],
                    Description = "Spicy juicy porkchops with carrots",
                    Image = "http://media-cdn.tripadvisor.com/media/photo-s/01/f5/21/e9/main-dish-of-rostbraten.jpg",
                    Name = "Pork chops with carrots",
                    UserId = user.Id
                },
                // desserts
                new Recipe()
                {
                    CategoryId = categories[3],
                    Description = "The best homemade Tiramisu cake ever greeneyed one !",
                    Image = "http://upload.wikimedia.org/wikipedia/commons/8/83/Tiramisu_with_cholocate_sauce_at_Ferrara_in_Little_Italy,_New_York_City.jpg",
                    Name = "Tiramisu cake",
                    UserId = user.Id
                },
                // desserts
                new Recipe()
                {
                    CategoryId = categories[3],
                    Description = "Homemade Creme Caramel",
                    Image = "http://files.meilleurduchef.com/mdc/photo/recipe/creme-caramel/creme-caramel-640.jpg",
                    Name = "Creme Caramel",
                    UserId = user.Id
                }
            };

            foreach (var recipie in recipies)
            {
                this.Data.Recipies.Add(recipie);
            }

            foreach (var recipie in recipies)
            {
                this.Data.Comments.Add(new Comment() { Text = "Great stuff yo!", RecipeId = recipie.Id, UserId = user.Id });
                this.Data.Comments.Add(new Comment() { Text = "Magic stuff", RecipeId = recipie.Id, UserId = user.Id });
                this.Data.Comments.Add(new Comment() { Text = "Unicorns in my stomach", RecipeId = recipie.Id, UserId = user.Id });
                this.Data.Comments.Add(new Comment() { Text = "I can feel the butterflies", RecipeId = recipie.Id, UserId = user.Id });
                this.Data.Comments.Add(new Comment() { Text = "Kinda works", RecipeId = recipie.Id, UserId = user.Id });

                this.Data.RecipeViews.Add(new RecipeView() { Liked = true, RecipeId = recipie.Id, UserId = user.Id });
                this.Data.RecipeViews.Add(new RecipeView() { Liked = true, RecipeId = recipie.Id, UserId = user.Id });
                this.Data.RecipeViews.Add(new RecipeView() { Liked = true, RecipeId = recipie.Id, UserId = user.Id });
                this.Data.RecipeViews.Add(new RecipeView() { Liked = false, RecipeId = recipie.Id, UserId = user.Id });
                this.Data.RecipeViews.Add(new RecipeView() { Liked = false, RecipeId = recipie.Id, UserId = user.Id });
                this.Data.RecipeViews.Add(new RecipeView() { Liked = false, RecipeId = recipie.Id, UserId = user.Id });
                this.Data.RecipeViews.Add(new RecipeView() { Liked = null, RecipeId = recipie.Id, UserId = user.Id });
                this.Data.RecipeViews.Add(new RecipeView() { Liked = null, RecipeId = recipie.Id, UserId = user.Id });
                this.Data.RecipeViews.Add(new RecipeView() { Liked = null, RecipeId = recipie.Id, UserId = user.Id });

                this.Data.PreparationSteps.Add(new PreparationStep() { Minutes = 5, RecipeId = recipie.Id, StepNumber = 1, Text = "Prepare mentaly" });
                this.Data.PreparationSteps.Add(new PreparationStep() { Minutes = 5, RecipeId = recipie.Id, StepNumber = 2, Text = "Find clean dishes" });
                this.Data.PreparationSteps.Add(new PreparationStep() { Minutes = 5, RecipeId = recipie.Id, StepNumber = 3, Text = "Pay your electricity bill" });
                this.Data.PreparationSteps.Add(new PreparationStep() { Minutes = 5, RecipeId = recipie.Id, StepNumber = 4, Text = "Clean the least dirty dishes" });
                this.Data.PreparationSteps.Add(new PreparationStep() { Minutes = 5, RecipeId = recipie.Id, StepNumber = 5, Text = "Cook the damn thing" });
            }

            this.Data.SaveChanges();
        }
    }
}