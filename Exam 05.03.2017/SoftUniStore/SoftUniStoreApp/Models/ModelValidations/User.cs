using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SoftUniStoreApp.Models
{
    public partial class User
    {
        private static bool IsEmailValid(string email)
        {
            string pattern = @".+@.+\..+";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(email);
        }

        private static bool IsPasswordValid(string password)
        {
            string pattern = @"(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}";
            Regex regex = new Regex(pattern);

            if (regex.IsMatch(pattern))
            {
                return true;
            }

            return false;
        }

        private static bool IsFullNameValid(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                return false;
            }

            return true;
        }
    }
}
