namespace ByTheCake
{
    using System;
    using System.IO;
    class ByTheCake
    {
        private static string path = "ByTheCake.html";
        static void Main()
        {
            Console.WriteLine("Content-Type: text/html\r\n");

            string[] html = File.ReadAllLines(path);

            foreach (string htmlLine in html)
            {
                Console.WriteLine(htmlLine);
            }
        }
    }
}
