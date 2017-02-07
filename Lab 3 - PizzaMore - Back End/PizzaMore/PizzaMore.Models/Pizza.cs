namespace PizzaMore.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Pizza
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Recipe { get; set; }

        public string ImageUrl { get; set; }

        public int UpVotes { get; set; }

        public int DownVotes { get; set; }

        public int OwnerId { get; set; }

        public virtual User Owner { get; set; }
    }
}
