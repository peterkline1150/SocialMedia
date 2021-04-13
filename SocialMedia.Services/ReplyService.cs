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

        public IEnumerable<ReplyList> GetRepliesByAuthorId(Guid authorId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Replies.Where(e => e.AuthorId == authorId)
                    .Select(
                        e => new ReplyList
                        {
                            Text = e.Text
                        }
                    );

                return query.ToArray();
            }
        }

        public bool UpdateReply(ReplyUpdate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Replies.Single(e => e.Id == model.Id);

                entity.Text = model.Text;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteReply(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Replies.Single(e => e.Id == id);

                ctx.Replies.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
