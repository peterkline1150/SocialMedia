using SocialMedia.Data;
using SocialMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Services
{
    public class ReplyService
    {
        private readonly Guid _userId;

        public ReplyService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateReply(ReplyCreate model)
        {
            var entity = new Reply()
            {
                AuthorId = _userId,
                Text = model.Text,
                CommentId = model.CommentId
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Replies.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ReplyList> GetReplies(int commentId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Replies.Where(e => e.CommentId == commentId)
                    .Select(
                        e => new ReplyList
                        {
                            Text = e.Text
                        }
                    );

                return query.ToArray();
            }
        }
    }
}
