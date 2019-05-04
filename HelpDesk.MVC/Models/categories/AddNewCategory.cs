using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.MVC.Models.categories
{
    public class AddNewCategory
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="نام گروه خالیست")]
        
        [StringLength(100,MinimumLength =3,ErrorMessage ="طول رشته باید حداقل 3 حرف باشد")]
        public string Name { get; set; }
        [StringLength(200,ErrorMessage ="حداکثر طول رشته نباید از 200 کاراکتر بیشتر باشد")]
        public string Description { get; set; }
    }
}
