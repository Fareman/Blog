using System.Collections.Generic;
using System.Linq;

namespace Blog
{
    public static class Answers
    {
        public static IEnumerable<string> NumberOfCommentsPerUser(MyDbContext context)
        {
            return context.BlogComments
                .GroupBy(c => c.UserName)
                .Select(g => $"{g.Key}: {g.Count()}");
        }

        public static IEnumerable<string> PostsOrderedByLastCommentDate(MyDbContext context)
        {
            return context.BlogPosts
                .OrderByDescending(p => p.Comments.Max(c => c.CreatedDate))
                .ToDictionary(p => p.Title, p => p.Comments.OrderByDescending(c => c.CreatedDate).FirstOrDefault())
                .Select(p => $"{p.Key}: \'{p.Value.CreatedDate}\', \'{p.Value.Text}\'");
        }

        public static IEnumerable<string> NumberOfLastCommentsLeftByUser(MyDbContext context)
        {
            return context.BlogPosts
                .Select(p => p.Comments.OrderByDescending(c => c.CreatedDate).FirstOrDefault())
                .GroupBy(c => c.UserName)
                .Select(g => $"{g.Key}: {g.Count()}");
        }
    }
}
