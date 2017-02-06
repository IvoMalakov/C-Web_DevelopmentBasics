namespace SignIn
{
    using System;
    using System.IO;
    class SignIn
    {
        private static string htmlPath = "../www/signIn.html";
        static void Main()
        {
            Console.WriteLine("Content-Type: text/html; charset=windows-1251\r\n");
            string htmlText = File.ReadAllText(htmlPath);
            Console.WriteLine(htmlText);
        }
    }
}
