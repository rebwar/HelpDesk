using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpDesk.Domain.Contracts.Articles;
using HelpDesk.Domain.Contracts.Categories;
using HelpDesk.Domain.Core.Articles;
using HelpDesk.Domain.Core.Categories;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.MVC.Controllers
{
    public class TestController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IArticleRepository articleRepository;
        public TestController(ICategoryRepository categoryRepository,IArticleRepository articleRepository)
        {
            this.categoryRepository = categoryRepository;
            this.articleRepository = articleRepository;
        }
        public IActionResult Index()
        {
           
          
            return View();
        }
        public List<Category> SearchCategory(string search)
        {
            return categoryRepository.SearchCategory(search).ToList();
        }
        public List<Article> SearchArticle(string search)
        {
            return articleRepository.SearchArticle(search).ToList();
        }
        [Route("test/searchresult/{term?}")]

        public IActionResult SearchResult(string term="")
        {

            var article = articleRepository.SearchArticle(term).ToList();
            return View(article);
        }
        public IActionResult SearchDetail(int id)
        {
            var model = articleRepository.Get(id);
            return View(model);
        }

    }
}