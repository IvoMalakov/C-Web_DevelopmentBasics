using System.Linq;
using SimpleHttpServer.Models;
using SimpleHttpServer.Utilities;
using SoftUniStoreApp.Data;
using SoftUniStoreApp.Helpers;
using SoftUniStoreApp.Models;

namespace SoftUniStoreApp.Security
{
    public static class AuthenticationManager
    {
        private static SoftUniStoreContext dbContext = Data.Data.Context;

        public static void LogoutUser(HttpResponse response, string sessionId)
        {
            Login login = dbContext.Logins.FirstOrDefault(l => l.IsActive && l.SessionId == sessionId);
            login.IsActive = false;
            dbContext.SaveChanges();

            HttpSession session = SessionCreator.Create();
            Cookie sessionCookie = new Cookie("sessionId", session.Id + "; HttpOnly; path=/");
            response.Header.AddCookie(sessionCookie);
        }

        public static bool IsAuthenticated(string sessionId)
        {
            return dbContext.Logins.Any(l => l.IsActive && l.SessionId == sessionId);
        }

        public static User GetAuthenticateduser(string sessionId)
        {
            User userAuthenticated = dbContext.Logins.FirstOrDefault(l => l.IsActive && l.SessionId == sessionId).User;
            ViewBag.Bag["email"] = userAuthenticated.Email;

            return userAuthenticated;
        }
    }
}
