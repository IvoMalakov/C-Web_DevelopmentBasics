namespace Calculator
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    class Calculator
    {
        private static string htmlPath = "Calculator.html";
        private static readonly string[] MathSingns = {"+", "-", "*", "/"};

        static void Main()
        {
            Console.WriteLine("Content-Type: text/html\r\n");
            string htmlString = File.ReadAllText(htmlPath);
            Console.WriteLine(htmlString);

            string inputString = Console.ReadLine();
            string[] inputStringAsArray = inputString.Split(new char[] {'&'}, StringSplitOptions.RemoveEmptyEntries);

            IList<string> parametreList = new List<string>();

            foreach (var inputInfo in inputStringAsArray)
            {
                string[] inputInfoAsArray = inputInfo.Split(new char[] {'='}, StringSplitOptions.RemoveEmptyEntries);
                string parameterForAdd = inputInfoAsArray[1];
                parametreList.Add(parameterForAdd);
            }

            string mathSign = WebUtility.UrlDecode(parametreList[1]);

            Console.WriteLine("<br>\r\n");

            if (!MathSingns.Contains(mathSign))
            {
                Console.WriteLine("<p>Invalid sign!</p>");
            }

            else
            {
                decimal firstNumber = decimal.Parse(parametreList[0]);
                decimal secondNumber = decimal.Parse(parametreList[2]);
                decimal result = Decimal.MinValue;

                switch (mathSign)
                {
                    case "+":
                        result = firstNumber + secondNumber;
                        break;

                    case "-":
                        result = firstNumber - secondNumber;
                        break;

                    case "*":
                        result = firstNumber * secondNumber;
                        break;

                    case "/":
                        if (secondNumber != 0)
                        {
                            result = firstNumber / secondNumber;
                        }

                        else
                        {
                            throw new ArithmeticException("Division by zero Exception");
                        }
                        break;

                    default:
                        throw new ArgumentException("Error! Invalid mathematic sign.");
                }

                Console.WriteLine($"<p>Result: {result:F2}</p>");
            }
        }
    }
}
