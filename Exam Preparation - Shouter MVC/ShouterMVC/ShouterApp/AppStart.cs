using SimpleHttpServer;
using SimpleMVC;

namespace ShouterApp
{
    class AppStart
    {
        static void Main()
        {
            HttpServer server = new HttpServer(8081, RouterTable.Routes);
            MvcEngine.Run(server, "ShouterApp");
        }
    }
}
