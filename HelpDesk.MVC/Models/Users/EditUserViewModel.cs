using HelpDesk.PublicClass;
using System.ComponentModel.DataAnnotations;

namespace HelpDesk.MVC.Models.Users
{
    public class EditUserViewModel
    {
        public string Id { get; set; }


        [Display(Name = "نام")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        public string FirstName { get; set; }

        [Display(Name = "فامیلی")]
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
        public string Email { get; set; }

        [Display(Name = "تصویر")]
        public string UserImage { get; set; }

        [Display(Name = "تاریخ تولد")]
        [Required(AllowEmptyStrings = false, ErrorMessage = PublicConst.EnterMessage)]
        public string BirthDayDate { get; set; }

        public bool ResetPass { get; set; }
    }
}
