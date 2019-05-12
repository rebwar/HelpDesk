using HelpDesk.PublicClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.MVC.Models.Login
{
    public class LoginViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(AllowEmptyStrings = false,ErrorMessage = PublicConst.EnterMessage)]
        public string UserName { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "مرا بخاطر بسپار")]
        public bool RememberMe { get; set; }
    }
}
