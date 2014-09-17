namespace MasterChef.Service
{
    using System;
    using System.Linq;
    using System.ServiceModel;
    using System.Data.Entity;
    using System.ServiceModel.Web;
    using PubNubMessaging.Core;

    using MasterChef.Data;

    public class RecipeService : IRecipeService
    {
        private const string Channel = "CookChanel";
        private static int counter = 0; //channels to be different everytime, however is the same recipe
        private RecipeInfo recipe;
        private Pubnub pubnub;
        private string channelName;

        public RecipeService()
        {
            this.Id = "";
            this.recipe = null;
            this.pubnub = new Pubnub("pub-c-15409870-ef42-4da1-bc2d-4211862362f3", "sub-c-e6269c5c-3d90-11e4-87bf-02ee2ddab7fe", "sec-c-MjAyYzc5ZjUtMmNmNC00Y2UyLTk4YmItODM2MDljZGRmZTMw");
            this.pubnub.Origin = "pubsub.pubnub.com";
            this.channelName = Channel;
        }

        public string Id { get; private set; }

        //The user must already have the subscribe key, so when we give him the channel name, 
        //the method in the client must delete the publish key, to prevent entering text in the channel
        public string GiveChannelName(Guid id)
        {
            this.Id = id.ToString();
            this.recipe = TakeRecipeSteps(id);
            this.channelName = Channel + this.Id + counter;
            counter++;

            return channelName; //to prevent easyer listening different channels
        }

        public void StartCoocking()
        {
            //TODO: 
            foreach (var step in recipe.PreparationSteps)
            {
                pubnub.Publish(channelName, step, (x => x = ""), (x => Console.WriteLine(x.Description)));  // error message may be must be differrent
            }
        }

        public string FinishedCooking()
        {
            return Channel; //or may be must throw the publish key to can write in the default chanel
        }
        
        private RecipeInfo TakeRecipeSteps(Guid id)
        {
            var db = new MasterChefDbContext(); //? MasterChefData
            var recipe = db.Recipes.Where(x => x.Id == id).Select(x =>
                new RecipeInfo
                {
                    Name = x.Name,
                    PreparationSteps = x.PreparationSteps
                })
                .FirstOrDefault();

            this.Id = id.ToString();
            counter++;

            return recipe;
        }
    }
}
