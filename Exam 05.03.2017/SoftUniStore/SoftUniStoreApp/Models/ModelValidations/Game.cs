using System.Text.RegularExpressions;

namespace SoftUniStoreApp.Models
{
    public partial class Game
    {
        public static bool IsTitleValid(string title)
        {
            string patter = @"[A-Z]{1}+.";
            Regex regex = new Regex(patter);

            if (title.Length >= 3 && title.Length <= 100 && regex.IsMatch(title))
            {
                return true;
            }

            return false;
        }

        public static bool IsDecimalValueValid(decimal decimalValue)
        {
            return decimalValue > 0.0m;
        }

        public static bool IsDescriptionValid(string description)
        {
            return description.Length >= 20;
        }

        public static bool IsImageThumbnailValid(string thumbnail)
        {
            string pattern = @"((https:\/\/)|(http:\/\/)).+";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(thumbnail);
        }

        public static string TakeTrailer(string thumbnail)
        {
            int equalsIndex = thumbnail.IndexOf('=');
            string trailer = thumbnail.Substring(equalsIndex + 1);
            return trailer;
        }

        public static bool IsTrailerValid(string trailer)
        {
            return trailer.Length == 11;
        }
    }
}
