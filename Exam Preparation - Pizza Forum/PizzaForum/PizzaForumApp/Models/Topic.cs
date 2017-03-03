using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaForumApp.Models
{
    public class Topic
    {
        public Topic()
        {
            this.Replies = new HashSet<Reply>();
        }

        [Key]
        public int Id { get; set; }

        public virtual User Author { get; set; }

        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public virtual ICollection<Reply> Replies { get; set; }
    }
}
