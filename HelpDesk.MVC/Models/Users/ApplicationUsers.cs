using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HelpDesk.MVC.Models.Users
{
    public class ApplicationUsers:IdentityUser<int>
    {
        [Display(Name = "نام")]
        public string FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]

        public string LastName { get; set; }
        [Display(Name = "تاریخ تولد")]

        public string BirthDate { get; set; }
        [Display(Name = "تلفن")]

        public string Phone { get; set; }
        [Display(Name = "جنسیت")]

        public byte Gender { get; set; }
        [Display(Name = "تصویر پروفایل")]

        public string ProfileImage { get; set; }

    }
}
