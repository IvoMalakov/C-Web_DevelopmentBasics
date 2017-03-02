using System.Linq;
using ShouterApp.BindingModels;
using ShouterApp.Data;
using ShouterApp.Data.Contracts;
using AutoMapper;
using ShouterApp.Models;

namespace ShouterApp.Services
{
    public class UserService
    {
        private IShouterContext context;

        public UserService() :
            this(new ShouterContext())
        {
            
        }

        public UserService(IShouterContext context)
        {
            this.context = context;
        }

        public void RegisterUser(RegisterUserBindingModel model)
        {
            if (model.Password == model.ConfirmPassword &&
                this.context.Users.FirstOrDefault(u => u.Username == model.Username) != null)
            {
                ConfigureMapper();
                User user = Mapper.Map<User>(model);

                this.context.Users.Add(user);
                this.context.SaveChanges();
            }
        }

        private void ConfigureMapper()
        {
            Mapper.Initialize(expression => expression.CreateMap<RegisterUserBindingModel, User>());

        }
    }
}
