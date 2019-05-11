using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HelpDesk.MVC.Models;
using HelpDesk.Domain.Core.Articles;
using HelpDesk.Domain.Contracts.Articles;
using HelpDesk.Domain.Core.Categories;
using HelpDesk.InfraStructures.DataAccess.Common;
using HelpDesk.MVC.Models.categories;
using HelpDesk.Domain.Contracts.Categories;

namespace HelpDesk.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleRepository articleRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly HelpDeskContext context;
        public HomeController(ICategoryRepository categoryRepository, IArticleRepository articleRepository, HelpDeskContext context)
        {
            this.articleRepository = articleRepository;
            this.context = context;
            this.categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            DesplayCategoryCount desplayCategory = new DesplayCategoryCount();
            desplayCategory.Categories = categoryRepository.GetAll().ToList();
            desplayCategory.Articles = articleRepository.GetAll().ToList();


           var Counts = from c in desplayCategory.Categories
                                           join p in desplayCategory.Articles on c.Id equals p.CategoryId
                                           group p by c.Name into g
                                           select new
                                           {
                                               Name = g.Key,
                                               Count = g.Count()
                                           }.Name.ToList();
            var TopArticles = articleRepository.GetTopViewedArticles(5);
            return View(TopArticles);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult SearchResult(string articleTitle)
        {

            var article = articleRepository.Search(articleTitle).ToList();
            return View(article);
        }
    }
}
