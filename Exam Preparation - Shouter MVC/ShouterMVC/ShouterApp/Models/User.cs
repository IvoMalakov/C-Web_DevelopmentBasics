using System.Collections.Generic;

namespace ShouterApp.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public  partial class User
    {
        private string username;
        private string email;
        private string password;
        private DateTime birthdate;

        public User()
        {
            this.Following = new HashSet<User>();
            this.Notifications = new HashSet<Notification>();
            this.Shaouts = new HashSet<Shaout>();
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
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(username, "username can not be null or empty");
                }

                else if (!IsUserNameValid(value))
                {
                    throw new ArgumentException("Username must contains only alphabetic characters");
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
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(email, "email can not be null or empty");
                }

                else if (!IsEmailValid(value))
                {
                    throw new ArgumentException("Email must conatains the sybmol \"@\"");
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
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(password, "password can not be null or empty");
                }

                else if (!IsPasswordValid(value))
                {
                    throw new ArgumentException("Your password must be atlease 3 symbols long");
                }

                this.password = value;
            }
        }

        public DateTime BirthDate
        {
            get
            {
                return this.birthdate;
            }

            set
            {
                if (!IsBirthDateValid(value))
                {
                    throw new ArgumentException("You must be at least 13 years old to register");
                }

                this.birthdate = value;
            }
        }

        public virtual ICollection<User> Following { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; } 

        public virtual ICollection<Shaout> Shaouts { get; set; }
    }
}
