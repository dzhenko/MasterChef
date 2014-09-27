namespace MasterChef.IntegrationTests
{
    using System;
    using System.Linq;
    using System.Net;
    using MasterChef.Data;
    using MasterChef.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Telerik.JustMock;

    [TestClass]
    public class RecipesControllerIntegrationTests
    {
        private static Random rand = new Random();
        private string inMemoryServerUrl = "http://localhost:12345";

        [TestMethod]
        public void GetAll_WhenBugsInDb_ShouldReturnStatus200AndNonEmptyContent()
        {
            IMasterChefData data = Mock.Create<IMasterChefData>();
            Recipe[] recipes = { new Recipe(), new Recipe(), new Recipe() };

            Mock.Arrange(() => data.Recipies.All())
                .Returns(() => recipes.AsQueryable());

            var server = new InMemoryHttpServer(this.inMemoryServerUrl, data);

            var response = server.CreateGetRequest("/api/Recipes");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Content);
        }

        [TestMethod]
        public void GetAll_WhenWrongPath_ShouldReturnStatus404()
        {
            IMasterChefData data = Mock.Create<IMasterChefData>();
            Recipe[] recipes = { new Recipe(), new Recipe(), new Recipe() };

            Mock.Arrange(() => data.Recipies.All())
                .Returns(() => recipes.AsQueryable());

            var server = new InMemoryHttpServer(this.inMemoryServerUrl, data);

            var response = server.CreateGetRequest("/api/Recipeswrong");

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}