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

            List<NameCount> nameCount = new List<NameCount>();
            var test = from c in desplayCategory.Categories
                       join p in desplayCategory.Articles on c.Id equals p.CategoryId
                       group p by c.Name into g
                       select new NameCount
                       {
                           Name = g.Key,
                           ArticleTitle=g.Take(5).ToList() ,
                           Count=g.Count()
                           };
            nameCount = test.ToList();

            ViewBag.Tops = nameCount;              
                
            
            var TopArticles = articleRepository.GetTopViewedArticles(5);
            return View(nameCount);
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
        public List<Article> SearchArticle(string search)
        {
            return articleRepository.SearchArticle(search).ToList();
        }
        [Route("Home/searchresult/{term?}")]

        public IActionResult SearchResult(string term = "")
        {

            var article = articleRepository.SearchArticle(term).ToList();
            return View(article);
        }
        public IActionResult SearchDetail(int id)
        {
            var model = articleRepository.Get(id);
            Article article = new Article();
            article = model;
            article.ViewCount = article.ViewCount + 1;                
            articleRepository.Update(model);
            return View(model);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult SearchDetail(int id,Article article)
        //{
        //    var model = articleRepository.Get(id);
        //    article.ViewCount = article.ViewCount + 1;
        //    articleRepository.Update(article);
        //    return View(model);
        //}
    }
}
