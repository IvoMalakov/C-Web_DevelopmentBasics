namespace PizzaMore.Utility
{
    using System.Text;
    using System.IO;
    public static class Logger
    {
        private static string LogFilePath = "../www/log.txt";
        private static void Log(string message)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(message);
            File.AppendAllText(LogFilePath, sb.ToString());
        }
    }
}
