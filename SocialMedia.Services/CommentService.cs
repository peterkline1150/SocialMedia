using SocialMedia.Data;
using SocialMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Services
{
    public class CommentService
    {
        private readonly Guid _userId;

        public CommentService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateComment(CommentCreate model)
        {
            var entity =
                new Comment()
                {
                    AuthorID = _userId,
                    PostId = model.PostId,
                    Text = model.Text,
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Comments.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CommentList> GetCommentsByPostId(int postId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Comments.Where(e => e.PostId == postId)
                    .Select(e => new CommentList
                    {
                        Text = e.Text,
                        Replies = e.Replies
                    }
                        
                    );

                return query.ToArray();
            }

        }
        public IEnumerable<CommentList> GetCommentsByAuthorId(Guid authorId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Comments.Where(e => e.AuthorID == authorId)
                    .Select(e => new CommentList
                    {
                        Text = e.Text,
                        Replies = e.Replies
                    }

                    );

                return query.ToArray();
            }

        }

        public bool UpdateComment(CommentUpdate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Comments.Single(e => e.ID == model.Id);

                entity.Text = model.Text;

                return ctx.SaveChanges() == 1;

            }
        }

        public bool DeleteComment(int Id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Comments.Single(e => e.ID == Id);
                ctx.Comments.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }


    }
}
