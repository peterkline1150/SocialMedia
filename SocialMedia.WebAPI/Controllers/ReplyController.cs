using Microsoft.AspNet.Identity;
using SocialMedia.Models;
using SocialMedia.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SocialMedia.WebAPI.Models
{
    [Authorize]
    public class ReplyController : ApiController
    {
        private ReplyService CreateReplyService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var replyService = new ReplyService(userId);
            return replyService;
        }

        public IHttpActionResult Post(ReplyCreate reply)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var service = CreateReplyService();

            if (!service.CreateReply(reply))
            {
                return InternalServerError();
            }

            return Ok();
        }

        public IHttpActionResult Get(int commentId)
        {
            ReplyService replyService = CreateReplyService();
            var replies = replyService.GetReplies(commentId);
            return Ok(replies);
        }

        public IHttpActionResult Get(Guid authorId)
        {
            ReplyService replyService = CreateReplyService();
            var replies = replyService.GetRepliesByAuthorId(authorId);
            return Ok(replies);
        }

        public IHttpActionResult Put(ReplyUpdate reply)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var service = CreateReplyService();

            if (!service.UpdateReply(reply))
            {
                return InternalServerError();
            }

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreateReplyService();

            if (!service.DeleteReply(id))
            {
                return InternalServerError();
            }

            return Ok();
        }
    }
}
