using SimpleHttpServer.Enums;

namespace SimpleHttpServer
{
    using System.IO;
    using Models;
    public static class ResponseBuilder
    {
        public static HTTPResponse InternalServerError()
        {
            string content = File.ReadAllText("../../resources/pages/505.html");
            HTTPResponse response = new HTTPResponse(ResponseStatusCode.Internal_Server_Error);
            response.ContentAsUtf8 = content;

            return response;
        }

        public static HTTPResponse NotFound()
        {
            string content = File.ReadAllText("../../resources/pages/404.html");
            HTTPResponse response = new HTTPResponse(ResponseStatusCode.Not_Found);
            response.ContentAsUtf8 = content;

            return response;
        }
    }
}
