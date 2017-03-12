using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SimpleHttpServer.Models;
using SoftUniStoreApp.BindingModels;
using SoftUniStoreApp.Data;
using SoftUniStoreApp.Models;

namespace SoftUniStoreApp.Services
{
    public class HomeService : Service
    {
        public HomeService() : base()
        {
        }

        public bool RegisterUser(RegisterUserBindingModel model)
        {
            if (model.Password == model.ConfirmPassword)
            {
                User chekedUser = this.Context.Users.FirstOrDefault(u => u.Email == model.Email);

                if (chekedUser == null)
                {
                    User userForAdd = Mapper.Map<User>(model);
                    int usersCounter = this.Context.Users.Count();
                    userForAdd.IsAdmin = usersCounter == 0;

                    this.Context.Users.Add(userForAdd);
                    this.Context.SaveChanges();

                    return true;
                }

                return false;
            }

            return false;
        }

        public bool LoginUser(LoginUserBindingModel model, HttpSession session)
        {
            User userFromDb =
                this.Context.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

            if (userFromDb != null)
            {
                Login login = new Login()
                {
                    IsActive = true,
                    SessionId = session.Id,
                    User = userFromDb
                };

                this.Context.Logins.Add(login);
                this.Context.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
