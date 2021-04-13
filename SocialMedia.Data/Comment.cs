using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Data
{
    public class Comment
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public Guid AuthorID { get; set; }

        public virtual List<Reply> Replies { get; set; } = new List<Reply>();


        [ForeignKey(nameof(Post))]
        public int PostId { get; set; }

        public Post Post { get; set; }
    }
}
