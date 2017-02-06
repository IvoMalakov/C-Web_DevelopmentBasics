namespace PizzaMore.Utility
{
    using System.Text;
    using System.IO;
    public static class Logger
    {
        private static void Log(string message)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(message);
            File.AppendAllText(Constants.LogFilePath, sb.ToString());
        }
    }
}
