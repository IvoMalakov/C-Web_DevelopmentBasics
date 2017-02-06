namespace Home
{
    using System;
    using System.IO;
    class Home
    {
        private static string htmlPath = "../www/home.html";
        static void Main()
        {
            Console.WriteLine("Content-Type: text/html; charset=windows-1251\r\n");
            string htmlText = File.ReadAllText(htmlPath);
            Console.WriteLine(htmlText);
        }
    }
}
