namespace Menu
{
    using System;
    using System.IO;
    class Menu
    {
        private static string htmlPath = "../www/menu.html";
        static void Main()
        {
            Console.WriteLine("Content-Type: text/html; charset=windows-1251\r\n");
            string htmlText = File.ReadAllText(htmlPath);
            Console.WriteLine(htmlText);
        }
    }
}
