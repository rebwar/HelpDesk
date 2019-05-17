using HelpDesk.Domain.Core.Articles;
using HelpDesk.Domain.Core.Categories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.MVC.Models.Articles
{
    public class AddNewArticle
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "عتوان خالیست")]
        [StringLength(500, MinimumLength =5, ErrorMessage = "طول رشته باید حداقل 5 حرف باشد")]
        public string Title { get; set; }
        [StringLength(1000, ErrorMessage = "حداکثر طول رشته نباید از 1000 کاراکتر بیشتر باشد")]
        public string Abstract { get; set; }
        [Required(ErrorMessage = "بدنه متن خالیست")]
        public string Body { get; set; }
        public IFormFile Image { get; set; }
        [StringLength(60, ErrorMessage = "حداکثر طول رشته تاریخ نباید از 60 کاراکتر بیشتر باشد")]
        public string PublishDate { get; set; }
        public ArticleStatus Status { get; set; }
        public string ImagePath { get; set; }
        public int CategoryId { get; set; }
        public IFormFile Video { get; set; }
        public string VideoPath { get; set; }
        public string PDFPath { get; set; }
        public IFormFile PDF { get; set; }
        public string CategoryName { get; set; }
        public int AspNetUsersId { get; set; }


    }
    public class DisplayArticleCategory:AddNewArticle
    {
        public List<Category> CatForDisplay { get; set; }
    }

    public class AddNewArticleGetViewModel : AddNewArticle
    {
        public int SelectedCat { get; set; }
    }
   public class DisplayArticle: AddNewArticle
    {
        public List<Article> Articles { get; set; }
        public Article ResultArticle { get; set; }

    }
}
