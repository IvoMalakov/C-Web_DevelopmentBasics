using System;
using System.Text.RegularExpressions;

namespace ShouterApp.Models
{
    public partial class User
    {
        private static bool IsUserNameValid(string username)
        {
            string pattern = @"[a-zA-Z0-9]+";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(username);
        }

        private static bool IsEmailValid(string email)
        {
            string pattern = @".+@.+";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(email);
        }

        private static bool IsPasswordValid(string password)
        {
            return password.Length >= 3;
        }

        private static bool IsBirthDateValid(DateTime birthDate)
        {
            decimal age = (decimal) (DateTime.Now - birthDate).TotalDays / 365.2468m;

            return age >= 13m;
        }
    }
}
