using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PizzaForumApp.Models
{
    public partial class User
    {
        public static bool IsUserNameValid(string username)
        {
            string pattern = @"[a-z0-9]+";
            Regex regex = new Regex(pattern);

            if (username.Length >= 3 && regex.IsMatch(username))
            {
                return true;
            }

            return false;
        }

        private static bool IsEmailValid(string email)
        {
            string pattern = @".+@.+";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(email);
        }

        private static bool IsPasswordValid(string password)
        {
            string pattern = @"[0-9]+";
            Regex regex = new Regex(pattern);

            if (password.Length == 4 && regex.IsMatch(password))
            {
                return true;
            }

            return false;
        }
    }
}
