namespace LoginForm
{
    using System;
    using System.IO;
    using System.Collections.Generic;

    class LoginForm
    {
        private static string htmlPath = "LoginForm.html";

        static void Main()
        {
            Console.WriteLine("Content-Type: text/html\r\n");
            string htmlString = File.ReadAllText(htmlPath);
            Console.WriteLine(htmlString);

            string inputString = Console.ReadLine();
            string[] inputStringAsArray = inputString.Split(new char[] {'&'}, StringSplitOptions.RemoveEmptyEntries);

            IList<string> parametreList = new List<string>();

            foreach (string inputInfo in inputStringAsArray)
            {
                string[] inputInfoAsArray = inputInfo.Split(new char[] {'='}, StringSplitOptions.RemoveEmptyEntries);
                string parametreForAdd = inputInfoAsArray[1];
                parametreList.Add(parametreForAdd);
            }

            string userName = parametreList[0];
            string password = parametreList[1];

            Console.WriteLine($"<p>Hi {userName}, your password is {password}</p>");
        }
    }
}
