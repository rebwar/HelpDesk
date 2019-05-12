using HelpDesk.Domain.Core.Articles;
using HelpDesk.Domain.Core.Categories;
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

        public string ImagePath { get; set; }

    }
    public class DesplayCategoryCount
    {
        public int Id { get; set; }
        public List<Article> Articles { get; set; }
        public List<Category> Categories { get; set; }
        public List<int> Count { get; set; }
        public List<string> CategoryName { get; set; }
        public string ImagePath { get; set; }

    }
    public class NameCount:DesplayCategoryCount
    {
        public int Count { get; set; }
        public string Name { get; set; }
        public List<Article> ArticleTitle { get; set; }

    }
}
