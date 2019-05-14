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
using HelpDesk.MVC.Models.Login;
using Microsoft.AspNetCore.Identity;
using HelpDesk.MVC.Models.Users;
using HelpDesk.MVC.Models.Articles;

namespace HelpDesk.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleRepository articleRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly HelpDeskContext context;
        private readonly SignInManager<ApplicationUsers> _signInManager;
        private readonly UserManager<ApplicationUsers> _userManager;
        public HomeController(ICategoryRepository categoryRepository, IArticleRepository articleRepository, HelpDeskContext context, SignInManager<ApplicationUsers> signInManager,
            UserManager<ApplicationUsers> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            this.articleRepository = articleRepository;
            this.context = context;
            this.categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            DesplayCategoryCount desplayCategory = new DesplayCategoryCount();
            desplayCategory.Categories = categoryRepository.GetAll().ToList();
            //var query = from t in context.Articles
            //            orderby t.Id descending
            //            select t;

            //desplayCategory.Articles = articleRepository.GetAll().Distinct().ToList();
            desplayCategory.Articles = articleRepository.GetArticlesDesc();
            desplayCategory.ArticleId = context.Articles.Select(x => x.Id).ToList();
            List<NameCount> nameCount = new List<NameCount>();
            var test = (from c in desplayCategory.Categories
                       join p in desplayCategory.Articles on c.Id equals p.CategoryId
                       group p by c.Name into g                       
                       select new NameCount
                       {
                           
                           Name = g.Key,
                           ArticleTitle=g.Take(5).ToList() ,
                           Count=g.Count()
                           });
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
            DisplayArticle displayArticle = new DisplayArticle();
            Article article = new Article();
            article = model;
            article.ViewCount = article.ViewCount + 1;
            articleRepository.Update(model);
            displayArticle.ResultArticle = article;
            //displayArticle.ResultArticle = model;
           displayArticle.Articles=articleRepository.GetAll().Take(2).ToList();

            return View(displayArticle);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName,
                    model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    //Success Login
                    var user = await _userManager.FindByNameAsync(model.UserName);
                    var userRole = _userManager.GetRolesAsync(user).Result.ToArray();

                    // return Json(new { status = "success" });
                    return RedirectToAction("Index", "Admin");
                }
                {
                    ModelState.AddModelError("MyKey", "نام کاربری یا رمز عبور اشتباه است");
                    ViewBag.loginFailText = "نام کاربری یا رمز عبور اشتباه است";
                    //اگر اطلاعات کاربر اشتباه بود
                    return View();
                    //return Json(new { status = "fail" });
                    //Failed Login
                }
            }
            //return PartialView("_loginPartial",model);
            return Json(new { status = "fail2" });
        }

    }
}
