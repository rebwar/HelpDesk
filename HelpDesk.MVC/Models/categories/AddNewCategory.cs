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
        [Required]
        [StringLength(100,MinimumLength =3)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
    }
}
