using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShouterApp.BindingModels;
using ShouterApp.Data;
using ShouterApp.Data.Contracts;
using ShouterApp.Services;
using SimpleMVC.Attributes.Methods;
using SimpleMVC.Controllers;
using SimpleMVC.Interfaces;
using SimpleMVC.Interfaces.Generic;

namespace ShouterApp.Controllers
{
    public class UsersController : Controller
    {
        private IShouterContext context;

        public UsersController()
            : this(new ShouterContext())
        {
            
        }

        public UsersController(IShouterContext context)
        {
            this.context = context;
        }
    }
}
