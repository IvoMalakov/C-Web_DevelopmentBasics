namespace BrowseCakes
{
    using System;
    using System.IO;
    using System.Linq;

    class BrowseCakes
    {
        private static string htmlPath = "BrowseCakes.html";
        private static string databasePath = "database.scv";

        static void Main()
        {
            Console.WriteLine("Content-Type: text/html\r\n");
            string htmlText = File.ReadAllText(htmlPath);
            Console.WriteLine(htmlText);

            string informationString = Environment.GetEnvironmentVariable("QUERY_STRING");
            string[] informationArray = informationString.Split(new char[] {'='}, StringSplitOptions.RemoveEmptyEntries);
            string searchWord = informationArray[1];

            string[] databaseContent = File.ReadAllLines(databasePath);
            var filtered = databaseContent.Where(s => s.Contains(searchWord));

            Console.WriteLine("<br>\r\n");

            foreach (var filterCake in filtered)
            {
                string filterCakeFixed = filterCake.Replace(",", " $");
                Console.WriteLine($"{filterCakeFixed}\r\n");
                Console.WriteLine("<br>\r\n");
            }
        }
    }
}
