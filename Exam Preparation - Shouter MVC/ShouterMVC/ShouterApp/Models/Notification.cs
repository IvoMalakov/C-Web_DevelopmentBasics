using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShouterApp.Models
{
    public class Notification
    {
        public Notification()
        {
            this.Notified = new HashSet<User>();
        }

        [Key]
        public int Id { get; set; }

        public virtual User ShoutAuthor { get; set; }

        [InverseProperty("Notifications")]
        public virtual ICollection<User> Notified { get; set; }
    }
}
