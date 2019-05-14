using HelpDesk.PublicClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.MVC.Models.Users
{
    public class UserViewModel
    {
        //Create
        public string Id { get; set; }

        [Display(Name = "کد استخدامی")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [StringLength(50, MinimumLength = 5, ErrorMessage = PublicConst.LengthMessage)]
        public string UserName { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = PublicConst.LengthMessage)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار رمز عبور")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = PublicConst.LengthMessage)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "رمز عبور با تکرار آن یکسان نیست")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "نام")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        public string LastName { get; set; }

        [Display(Name = "جنسیت")]
        public byte gender { get; set; }

        [Display(Name = "تلفن")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "شماره تماس 11 رقمی می باشد")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "شماره تماس شامل حرف نمی تواند باشد")]
        public string PhoneNumber { get; set; }

        [Display(Name = "ایمیل")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "ایمیل را به درستی وارد کنید")]
        public string Email { get; set; }

        [Display(Name = "تصویر")]
        public string UserImage { get; set; }

        [Display(Name = "تاریخ تولد")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        public string BirthDayDate { get; set; }
    }
}
