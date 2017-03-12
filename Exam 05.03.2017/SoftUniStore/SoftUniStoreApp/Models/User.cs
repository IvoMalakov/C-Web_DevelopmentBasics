using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniStoreApp.Models
{
    public partial class User
    {
        private string email;
        private string password;
        private string fullName;

        public User()
        {
            this.OwnedGames = new HashSet<Game>();
        }

        [Key]
        public int Id { get; set; }

        public string Email
        {
            get
            {
                return this.email;
            }

            set
            {
                if (!IsEmailValid(value))
                {
                    throw new ArgumentException("Invalid email");
                }

                this.email = value;
            }
        }

        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (!IsPasswordValid(value))
                {
                    throw new ArgumentException("Invalid password");
                }

                this.password = value;
            }
        }

        public string FullName
        {
            get
            {
                return this.fullName;
            }

            set
            {
                if (!IsFullNameValid(value))
                {
                    throw new ArgumentException("Invalid full name");
                }

                this.fullName = value;
            }
        }

        public bool IsAdmin { get; set; }

        public virtual ICollection<Game> OwnedGames { get; set; }
    }
}
