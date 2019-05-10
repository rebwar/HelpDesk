using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpDesk.MVC.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUsers> userManager;
        public UserController(UserManager<ApplicationUsers> userManager)
        {
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult UserList()
        {
            var userlist = userManager.Users.Take(50).ToList();
            return View(userlist);
        }
    }
}