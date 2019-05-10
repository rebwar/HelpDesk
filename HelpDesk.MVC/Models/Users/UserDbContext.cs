using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.MVC.Models.Users
{
    public class UserDbContext : IdentityDbContext<ApplicationUsers, ApplicaionRoles, int>
    {
        public UserDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
