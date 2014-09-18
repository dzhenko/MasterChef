namespace MasterChef.Web.DataModels
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using System.ComponentModel.DataAnnotations;

    using MasterChef.Models;

    public class CommentDataModel
    {
        public static Func<Comment, CommentDataModel> FromDataToModel
        {
            get
            {
                return c => new CommentDataModel() 
                { 
                    User = c.User.UserName,
                    Text = c.Text
                };
            }
        }

        public static Comment FromModelToData(CommentDataModel model, string userId, Guid recipeId)
        {
            return new Comment()
            {
                RecipeId = recipeId,
                UserId = userId,
                Text = model.Text
            };
        }

        public string User { get; set; }

        [Required]
        public string Text { get; set; }
    }
}