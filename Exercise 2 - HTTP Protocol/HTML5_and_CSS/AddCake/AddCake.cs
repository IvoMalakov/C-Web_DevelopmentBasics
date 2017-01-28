namespace AddCake
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Text;
    class AddCake
    {
        private static string path = "AddCake.html";
        private static string databaseFilePath = "database.scv";
        private static void Main()
        {
            IList<Cake> cakes = new List<Cake>();

            Console.WriteLine("Content-Type: text/html\r\n");

            string[] html = File.ReadAllLines(path);

            foreach (string htmlLine in html)
            {
                Console.WriteLine(htmlLine);
            }

            string codeString = Console.ReadLine();

            if (codeString != null)
            {
                string[] information = codeString.Split(new char[] {'&'}, StringSplitOptions.RemoveEmptyEntries);

                IList<string> informationList = new List<string>();

                foreach (string parametre in information)
                {
                    string[] temp = parametre.Split(new char[] {'='}, StringSplitOptions.RemoveEmptyEntries);
                    string usefulInformation = temp[1];

                    informationList.Add(usefulInformation);
                }

                string name = informationList[0].Replace('+', ' ');
                decimal price = Convert.ToDecimal(informationList[1]);

                Cake cake = new Cake()
                {
                    Name = name,
                    Price = price
                };

                cakes.Add(cake);

                Console.WriteLine("<br>\r\n");
                Console.WriteLine($"name: {name}\r\n");
                Console.WriteLine("<br>\r\n");
                Console.WriteLine($"price: {price}\r\n");

                StringBuilder sb = new StringBuilder();

                foreach (Cake cakeForAdd in cakes)
                {
                    sb.AppendFormat("{0},{1}\r\n", cakeForAdd.Name, cakeForAdd.Price);
                }

                File.AppendAllText(databaseFilePath, sb.ToString());
            }
        }
    }
}
