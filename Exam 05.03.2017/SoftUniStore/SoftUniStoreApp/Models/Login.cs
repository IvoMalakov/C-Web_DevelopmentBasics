using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniStoreApp.Models
{
    public class Login
    {
        [Key]
        public int Id { get; set; }

        public bool IsActive { get; set; }

        public string SessionId { get; set; }

        public virtual User User { get; set; }
    }
}
