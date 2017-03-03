using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleHttpServer;
using SimpleMVC;

namespace PizzaForumApp
{
    class PizzaForumStart
    {
        static void Main(string[] args)
        {
            HttpServer server = new HttpServer(8081, RouterTable.Routes);
            MvcEngine.Run(server, "PizzaForumApp");
        }
    }
}
