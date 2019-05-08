using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using HelpDesk.Domain.Contracts.Articles;
using HelpDesk.Domain.Contracts.Categories;
using HelpDesk.Domain.Core.Articles;
using HelpDesk.Domain.Core.Categories;
using HelpDesk.InfraStructures.DataAccess.Common;
using HelpDesk.MVC.Models.Articles;
using HelpDesk.MVC.Models.categories;
using MD.PersianDateTime;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace HelpDesk.MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IArticleRepository articleRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public AdminController(ICategoryRepository categoryRepository, IArticleRepository articleRepository,IHostingEnvironment hostingEnvironment)
        {
            this.categoryRepository = categoryRepository;
            this.articleRepository = articleRepository;
            this._hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(AddNewCategory model)
        {
            if (ModelState.IsValid)
            {
                Category category = new Category
                {
                    Name = model.Name,
                    Description = model.Description
                };
                categoryRepository.Add(category);
            }
            return RedirectToAction(nameof(ListCat));
        }
        public IActionResult ListCat()
        {
            var cat = categoryRepository.GetAll().ToList();
           // ViewData["ArticlesCount"] = articleRepository.GetArticlesCountInCategory()
           return View(cat);
        }
        public IActionResult ListArticle()
        {
            var article = articleRepository.GetAll().ToList();
            return View(article);
        }
        public IActionResult EditCategory(int id)
        {
            return View();
        }
        public IActionResult Delete(int id)
        {
            try
            {
                var cat = categoryRepository.Get(id);
                categoryRepository.Delete(cat);
                return Ok();
            }
            catch (Exception exp)
            {
                return BadRequest();
            }
        }
        public IActionResult Details(int Id)
        {
            ViewBag.type = 1;
            var cat = categoryRepository.Get(Id);
            if (cat == null)
            {
                return BadRequest();
            }
            return PartialView("Details", cat);

        }
        public IActionResult Edit(int Id)
        {
            var cat = categoryRepository.Get(Id);
            if (cat == null)
            {
                return BadRequest();
            }
            return View(cat);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            categoryRepository.Update(category);
            return RedirectToAction(nameof(ListCat));
        }
        public IActionResult AddArticle()
        {
            DisplayArticleCategory cat = new DisplayArticleCategory();
            cat.CatForDisplay = categoryRepository.GetAll().ToList();
            return View(cat);
        }
        [HttpPost]
        public async Task<IActionResult> AddArticle(AddNewArticleGetViewModel model)
        {
            if (ModelState.IsValid)
            {
                var persiandate = new PersianDateTime(DateTime.Now);
                Article article = new Article
                {
                    Title = model.Title,
                    Abstract = model.Abstract,
                    Body = model.Body,
                    PublishDate = persiandate.ToString(),
                    Status = ArticleStatus.Publish,
                    CategoryId=model.SelectedCat
                };
                if ((model?.Image?.Length > 0) && ((model?.Image?.ContentType=="image/jpeg") || (model?.Image?.ContentType == "image/jpg")))
                {
                    string path_root = _hostingEnvironment.WebRootPath;
                    string path_to_image = path_root + "\\Images\\" + model.Image.FileName;
                    using (var stream = new FileStream(path_to_image, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(stream);
                    }
                    article.Image = @"~/images/" + model.Image.FileName;
                }
                articleRepository.Add(article);
            }
            return RedirectToAction(nameof(ListArticle));
        }
        public IActionResult DeleteArticle(int id)
        {
            try
            {
                var cat = articleRepository.Get(id);
                articleRepository.Delete(cat);
                return Ok();
            }
            catch (Exception exp)
            {
                return BadRequest();
            }
        }
        public IActionResult EditArticle(int Id)
        {
            var article = articleRepository.Get(Id);
            if (article == null)
            {
                return BadRequest();
            }
            else
            {
                DisplayArticleCategory displayArticleCategory = new DisplayArticleCategory();
                displayArticleCategory.CatForDisplay = categoryRepository.GetAll().ToList();
                displayArticleCategory.Abstract = article.Abstract;
                displayArticleCategory.Body = article.Body;
                displayArticleCategory.CategoryId = article.CategoryId;
                displayArticleCategory.Id = article.Id;
                displayArticleCategory.ImagePath = article.Image;
                displayArticleCategory.PublishDate = article.PublishDate;
                displayArticleCategory.Status = article.Status;
                displayArticleCategory.Title = article.Title;
                return View(displayArticleCategory);
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditArticle(AddNewArticleGetViewModel displayArticle, Article article)
        {
            var oldImagePath = displayArticle.ImagePath.Remove(0,9);
            article.CategoryId = displayArticle.SelectedCat;
            if ((displayArticle?.Image?.Length > 0) && ((displayArticle?.Image?.ContentType == "image/jpeg") || (displayArticle?.Image?.ContentType == "image/jpg")))
            {
                string path_root = _hostingEnvironment.WebRootPath;
                string path_Old_Image = path_root + "\\Images\\" + oldImagePath;
                string path_to_image = path_root + "\\Images\\" + displayArticle.Image.FileName;
                using (var stream = new FileStream(path_to_image, FileMode.Create))
                {
                    await displayArticle.Image.CopyToAsync(stream);
                }
                article.Image = @"~/images/" + displayArticle.Image.FileName;
                System.IO.File.Delete(path_Old_Image);

                articleRepository.Update(article);
                return RedirectToAction(nameof(ListArticle));
            }
            else
            {
                article.Image = displayArticle.ImagePath;
                articleRepository.Update(article);
                return RedirectToAction(nameof(ListArticle));

            }
        }
        public IActionResult ArticleDetails(int id)
        {
            var article = articleRepository.Get(id);
            
            if (article == null)
            {
                return BadRequest();
            }
            else
            {
                Category category = new Category();
                category= categoryRepository.Get(article.CategoryId);
                ViewData["Category"] = category.Name;
                return View(article);
            }

        }
        public IActionResult Upload(int id)
        {
            var article = articleRepository.Get(id);
            if (article == null)
            {
                return BadRequest();
            }
            else
            {
                Article model = new Article();
                model.Title = article.Title;
                return View(model);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file,Article article)
        {
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Images\\Multi");
            var request = 1;
            if (request == 1)
            {
                if (file.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }
            if(request==2)
            {
                var fullPath = Path.Combine(uploads, file.FileName);

                System.IO.File.Delete(fullPath);
                

            }
            return RedirectToAction("Index");
        }
        public IActionResult Upload2(int id)
        {
            var article = articleRepository.Get(id);
            if (article == null)
            {
                return BadRequest();
            }
            else
            {
                Article model = new Article();
                model.Title = article.Title;
                return View(model);
            }
        }
        [HttpPost]
        public void Upload2222(string name, int request)
        {
            var name2 = name;
        }

        [HttpPost]
        public async Task<IActionResult> Upload2(IFormFile file, Article article, string name, int request)
        {
            var request1 = request;
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Images\\Multi");
            int i = HttpContext.Request.Headers.Count;
            request = 1;
            if (request1 == 1)
            {
                if (file.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }
            if (request1 == 2)
            {
                var fullPath = Path.Combine(uploads, file.FileName);

                System.IO.File.Delete(fullPath);


            }
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //public async Task<IActionResult> Upload2(IFormFile file, Article article,string name, int request)
        //{
        //    var request1 = request;
        //    var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Images\\Multi");
        //    int i = HttpContext.Request.Headers.Count;
        //    request = 1;
        //    //halate pishfarze upload =1
        //    //inja mikham request ro ke post hast begiram o moqayesash konam ke age 2 bood
        //    //if (String.IsNullOrEmpty(HttpContext.Request.Headers.Count))
        //        if (request1 == 1)
        //    {
        //        if (file.Length > 0)
        //        {
        //            using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
        //            {
        //                await file.CopyToAsync(fileStream);
        //            }
        //        }
        //    }
        //    if (request1 == 2)
        //    {
        //        var fullPath = Path.Combine(uploads, file.FileName);

        //        System.IO.File.Delete(fullPath);


        //    }
        //    return RedirectToAction("Index");
        //}

    }
}