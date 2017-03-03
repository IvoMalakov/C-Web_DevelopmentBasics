using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaForumApp.Models
{
    public partial class User
    {
        private string username;
        private string email;
        private string password;

        public User()
        {
            this.Topics = new HashSet<Topic>();
        }

        [Key]
        public int Id { get; set; }

        public string Username
        {
            get
            {
                return this.username;
            }

            set
            {
                if (!IsUserNameValid(value))
                {
                    throw new ArgumentException("Invalid username");
                }

                this.username = value;
            }
        }

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

        public bool IsAdmin { get; set; }

        public virtual ICollection<Topic> Topics { get; set; }
    }
}
