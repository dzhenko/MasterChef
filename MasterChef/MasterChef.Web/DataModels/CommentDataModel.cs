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
                    Text = c.Text,
                    RecipeId = c.RecipeId.ToString()
                };
            }
        }

        public static Comment FromModelToData(CommentDataModel model, string userId)
        {
            return new Comment()
            {
                RecipeId = Guid.Parse(model.RecipeId),
                UserId = userId,
                Text = model.Text
            };
        }

        public string User { get; set; }

        [Required]
        public string Text { get; set; }

        public string RecipeId { get; set; }
    }
}