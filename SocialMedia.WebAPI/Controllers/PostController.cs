using Microsoft.AspNet.Identity;
using SocialMedia.Models;
using SocialMedia.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SocialMedia.WebAPI.Controllers
{
    [Authorize]
    public class PostController : ApiController
    {
        public IHttpActionResult Get()
        {
            PostService postService = CreatePostService();
            var posts = postService.GetPosts();
            return Ok(posts);
        }

        public IHttpActionResult Post(PostCreate post)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreatePostService();

            if (!service.CreatePost(post))
                return InternalServerError();

            return Ok();
        }

        private PostService CreatePostService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var postService = new PostService(userId);
            return postService;
        }

        public IHttpActionResult Get(Guid authorId)
        {
            PostService postService = CreatePostService();
            var posts = postService.GetPostsByAuthorId(authorId);
            return Ok(posts);
        }

        public IHttpActionResult Get(int PostId)
        {
            PostService postService = CreatePostService();
            var post = postService.GetPostByPostId(PostId);
            return Ok(post);
        }

        public IHttpActionResult Put(PostUpdate post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var service = CreatePostService();

            if (!service.UpdatePost(post))
            {
                return InternalServerError();
            }

            return Ok();
        }

        public IHttpActionResult Delete(int Id)
        {
            var service = CreatePostService();

            if (!service.DeletePost(Id))
            {
                return InternalServerError();
            }

            return Ok();
        }
    }
}
