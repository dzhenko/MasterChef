namespace MasterChef.Web.DataModels
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

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

        public static Func<CommentDataModel, Comment> FromModelToData
        {
            get
            {
                return c => new Comment()
                {
                    Text = c.Text
                };
            }
        }

        public string User { get; set; }

        public string Text { get; set; }
    }
}