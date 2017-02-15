namespace SimpleMVC.App.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class User
    {
        public User()
        {
            this.Notes = new List<Note>();
        }

        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public virtual ICollection<Note> Notes { get; set; }
    }
}
