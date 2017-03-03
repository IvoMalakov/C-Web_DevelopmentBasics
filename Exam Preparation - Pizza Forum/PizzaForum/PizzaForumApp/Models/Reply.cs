using System;
using System.ComponentModel.DataAnnotations;

namespace PizzaForumApp.Models
{
    public class Reply
    {
        [Key]
        public int Id { get; set; }

        public string Content { get; set; }

        public virtual User Author { get; set; }

        public DateTime PublishDate { get; set; }

        public string ImageUrl { get; set; }
    }
}
