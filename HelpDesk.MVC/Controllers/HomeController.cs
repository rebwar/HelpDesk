using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HelpDesk.MVC.Models;
using HelpDesk.Domain.Core.Articles;
using HelpDesk.Domain.Contracts.Articles;

namespace HelpDesk.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleRepository articleRepository;
        public HomeController(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }
        public IActionResult Index()
        {

            return View();
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
