using System.ComponentModel.DataAnnotations;

namespace ShouterApp.Models
{
    public class Login
    {
        [Key]
        public int Id { get; set; }

        public string SessionId { get; set; }

        public bool IsActive { get; set; }

        public virtual User User { get; set; }
    }
}
