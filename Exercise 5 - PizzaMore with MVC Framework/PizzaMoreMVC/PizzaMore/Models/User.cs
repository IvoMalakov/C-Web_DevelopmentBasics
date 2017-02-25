namespace PizzaMore.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class User
    {
        public User()
        {
            this.Suggestions = new HashSet<Pizza>();
        }

        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public virtual ICollection<Pizza> Suggestions { get; set; }
    }
}
