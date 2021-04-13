using SocialMedia.Data;
using SocialMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Services
{
    public class PostService
    {
        private readonly Guid _userId;

        public PostService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreatePost(PostCreate model)
        {
            var entity =
                new Post()
                {
                    AuthorId = _userId,
                    Title = model.Title,
                    Text = model.Text,
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Posts.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<PostList> GetPosts()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Posts
                        .Where(e => e.AuthorId == _userId)
                        .Select(
                            e =>
                                new PostList
                                {
                                    Id = e.Id,
                                    Title = e.Title,
                                    Text = e.Text
                                }
                        );

                return query.ToArray();
            }
        }

        public IEnumerable<PostList> GetPostsByAuthorId(Guid authorId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Posts
                        .Where(e => e.AuthorId == authorId)
                        .Select(
                            e =>
                                new PostList
                                {
                                    Id = e.Id,
                                    Title = e.Title,
                                    Text = e.Text
                                }
                        );

                return query.ToArray();
            }
        }

        public bool UpdatePost(PostUpdate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Posts.Single(e => e.Id == model.Id);

                entity.Title = model.Title;

                entity.Text = model.Text;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeletePost(int Id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Posts.Single(e => e.Id == Id);

                ctx.Posts.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
