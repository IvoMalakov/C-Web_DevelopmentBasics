namespace PizzaMore
{
    using SimpleHttpServer;
    using SimpleMVC;

    class PizzaMoreStart
    {
        static void Main(string[] args)
        {
            HttpServer server = new HttpServer(8081, RouterTable.Routes);
            MvcEngine.Run(server, "PizzaMore");
        }
    }
}
