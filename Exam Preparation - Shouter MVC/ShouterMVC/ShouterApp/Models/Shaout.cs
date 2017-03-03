using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShouterApp.Models
{
    public class Shaout
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(160)]
        public string Content { get; set; }

        [InverseProperty("Shaouts")]
        public virtual User Authour { get; set; }


        public DateTime? PostedOn { get; set; }

        public TimeSpan? LifeTime { get; set; }
    }
}
