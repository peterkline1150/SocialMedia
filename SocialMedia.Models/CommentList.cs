using SocialMedia.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Models
{
    public class CommentList
    {
        public string Text { get; set; }

        public virtual List<Reply> Replies { get; set; }
    }
}
