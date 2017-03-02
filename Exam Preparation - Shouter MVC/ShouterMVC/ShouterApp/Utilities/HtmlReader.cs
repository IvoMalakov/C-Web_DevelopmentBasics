using System.IO;

namespace ShouterApp.Utilities
{
    public static class HtmlReader
    {
        public static string Read(string path)
        {
            string htmlText = File.ReadAllText(path);
            return htmlText;
        }
    }
}
