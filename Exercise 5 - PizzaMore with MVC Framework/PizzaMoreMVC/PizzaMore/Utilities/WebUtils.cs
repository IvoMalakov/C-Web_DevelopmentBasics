namespace PizzaMore.Utilities
{
    using System.IO;

    public class WebUtils
    {
        public static string RetriveContentPathFolder(string path)
        {
            string content = File.ReadAllText(path);
            return content;
        }
    }
}
