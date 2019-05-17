using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using HelpDesk.Domain.Contracts.Articles;
using HelpDesk.Domain.Contracts.Categories;
using HelpDesk.Domain.Contracts.Images;
using HelpDesk.Domain.Core.Articles;
using HelpDesk.Domain.Core.Categories;
using HelpDesk.InfraStructures.DataAccess.Common;
using HelpDesk.MVC.Models.Articles;
using HelpDesk.MVC.Models.categories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HelpDesk.Domain.Core.Images;
using static System.Net.Mime.MediaTypeNames;
using HelpDesk.MVC.Service;
using MD.PersianDateTime.Standard;
using Microsoft.AspNetCore.Identity;
using HelpDesk.MVC.Models.Users;
using Microsoft.AspNetCore.Authorization;

namespace HelpDesk.MVC.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IArticleRepository articleRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUploadFileRepository uploadFileRepository;
        private readonly IimageRepository imageRepository;
        private readonly UserManager<ApplicationUsers> userManager;
        private readonly SignInManager<ApplicationUsers> _signInManager;
        private readonly RoleManager<ApplicaionRoles> roleManager;

        public AdminController(IimageRepository imageRepository, ICategoryRepository categoryRepository, IArticleRepository articleRepository,
            IUploadFileRepository uploadFileRepository, IHostingEnvironment hostingEnvironment,
            UserManager<ApplicationUsers> userManager, SignInManager<ApplicationUsers> signInManager
            , RoleManager<ApplicaionRoles> roleManager)
        {
            _signInManager = signInManager;
            this.roleManager = roleManager;
            this.categoryRepository = categoryRepository;
            this.articleRepository = articleRepository;
            this._hostingEnvironment = hostingEnvironment;
            this.uploadFileRepository = uploadFileRepository;
            this.imageRepository = imageRepository;
            this.userManager = userManager;
        }
        public IList<string> RolesByUser(ApplicationUsers User)
        {
            return userManager.GetRolesAsync(User).Result;

        }
        public IActionResult Index()
        {
            Statistics statistics = new Statistics();
            string UserId = userManager.GetUserId(HttpContext.User);
            UserRoleViewModel userRoleView = new UserRoleViewModel();
            userRoleView.User = userManager.FindByIdAsync(UserId).Result;

            IList<string> RolesBelongUser = RolesByUser(userRoleView.User);
            bool HasUserAdmin = false;
            foreach (var item in RolesBelongUser)
            {
                if (item == "Admin")
                {
                    HasUserAdmin = true;
                }
            }
            if (HasUserAdmin)
            {
                ViewBag.IsAdmin = true;
                statistics.PageVisited = articleRepository.GetVisitCount();
                statistics.Users = userManager.Users.Count();
                statistics.Categories = categoryRepository.GetStatistic();
                statistics.Articles = articleRepository.GetStatistic();

            }
            else
            {
                ViewBag.IsAdmin = false;

                statistics.PageVisited = articleRepository.GetVisitCountByAuthor(int.Parse(UserId));
                statistics.Articles = articleRepository.GetArticleStaticsByAuthor(int.Parse(UserId));
            }
            statistics.User = userManager.FindByIdAsync(UserId).Result;
            return View(statistics);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AddCategory()
        {
            return View();
        }
        public IActionResult UploadCatImage(IFormFile files)
        {
            string newFileName = uploadFileRepository.UplaodFile(files, "\\Images\\Category\\");
            return Json(new { status = "success", message = "تصویر با موفقیت آپلود شد.", imagename = newFileName });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public IActionResult AddCategory(AddNewCategory model, string imagename)
        {
            if (ModelState.IsValid)
            {
                if (imagename == null)
                {
                    model.ImagePath = "default.png";
                }
                else
                {
                    model.ImagePath = imagename;
                }
                Category category = new Category
                {
                    Name = model.Name,
                    Description = model.Description,
                    ImagePath = model.ImagePath
                };
                categoryRepository.Add(category);
            }
            return RedirectToAction(nameof(ListCat));
        }
        [Authorize(Roles = "Admin")]

        public IActionResult ListCat()
        {
            var cat = categoryRepository.GetAll().ToList();
            // ViewData["ArticlesCount"] = articleRepository.GetArticlesCountInCategory()
            return View(cat);
        }
        public IActionResult ListArticle()
        {
            string UserId = userManager.GetUserId(HttpContext.User);
            UserRoleViewModel userRoleView = new UserRoleViewModel();
            userRoleView.User = userManager.FindByIdAsync(UserId).Result;

            IList<string> RolesBelongUser = RolesByUser(userRoleView.User);
            bool HasUserAdmin = false;
            foreach (var item in RolesBelongUser)
            {
                if(item=="Admin")
                {
                    HasUserAdmin = true;
                }
            }
            if(HasUserAdmin)
            {
                var article = articleRepository.GetAll().ToList();
                return View(article);

            }
            else
            {
                var article = articleRepository.GetArticleByAuthor(int.Parse(UserId)).ToList();
                return View(article);

            }

        }
        [Authorize(Roles = "Admin")]
        public IActionResult EditCategory(int id)
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [ValidateAntiForgeryToken]
        public ActionResult AddArticle(AddNewArticleGetViewModel model)
        {
            if (ModelState.IsValid)
            {
                var persiandate = new PersianDateTime(DateTime.Now);
                string UserId = userManager.GetUserId(HttpContext.User);
                Article article = new Article
                {
                    Title = model.Title,
                    Abstract = model.Abstract,
                    Body = model.Body,
                    PublishDate = persiandate.ToString(),
                    Status = ArticleStatus.PrePublish,
                    CategoryId = model.SelectedCat,
                    Likes = 0,
                    ViewCount = 0,
                    AspNetUsersId=int.Parse(UserId)
                    

                };
                string FileName = "";

                if ((model?.Image?.Length > 0) && ((model?.Image?.ContentType == "image/jpeg") || (model?.Image?.ContentType == "image/jpg")))
                {
                    FileName = uploadFileRepository.UplaodFile(model.Image, "\\Images\\");
                    article.Image = @"~/images/" + FileName;
                }
                if ((model?.Video?.Length > 0) && (model?.Video?.ContentType == "video/mp4"))
                {
                    FileName = uploadFileRepository.UplaodFile(model.Video, "\\Video\\");
                    article.Video = @"~/video/" + FileName;
                }
                if ((model?.PDF?.Length > 0) && (model?.PDF?.ContentType == "application/pdf"))
                {
                    FileName = uploadFileRepository.UplaodFile(model.PDF, "\\Pdf\\");
                    article.PDF = @"~/pdf/" + FileName;
                }


                articleRepository.Add(article);
                return RedirectToAction(nameof(ListArticle));

            }
            else
            {
                return RedirectToAction(nameof(ListArticle));
            }
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
        public IActionResult EditArticleStatus(int Id)
        {
            var article = articleRepository.Get(Id);
            if (article == null)
            {
                return BadRequest();
            }
            else
            {
                DisplayArticleCategory displayArticleCategory = new DisplayArticleCategory();
                displayArticleCategory.Abstract = article.Abstract;
                displayArticleCategory.Id = article.Id;
                displayArticleCategory.PublishDate = article.PublishDate;
                displayArticleCategory.Status = article.Status;
                displayArticleCategory.Title = article.Title;
                return View(displayArticleCategory);
            }
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult EditArticleStatus( Article article,int Id)
        {

            if (ModelState.IsValid)
            {                
                Article article1 = new Article();
                article1= articleRepository.Get(Id);
                article1.Status = article.Status;
                articleRepository.Update(article1);
                return RedirectToAction(nameof(ListArticle));
            }
            else
            {
                return View(article);

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
                displayArticleCategory.AspNetUsersId = article.AspNetUsersId;
                
                return View(displayArticleCategory);
            }
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult EditArticle(AddNewArticleGetViewModel displayArticle, Article article)
        {
            if (ModelState.IsValid)
            {
                var oldImagePath = displayArticle.ImagePath.Remove(0, 9);
                

                article.CategoryId = displayArticle.SelectedCat;
                string FileName = "";
                string path_root = _hostingEnvironment.WebRootPath;

                if ((displayArticle?.Image?.Length > 0) && ((displayArticle?.Image?.ContentType == "image/jpeg") || (displayArticle?.Image?.ContentType == "image/jpg")))
                {
                    string path_Old_Image = path_root + "\\Images\\" + oldImagePath;
                    string path_to_image = path_root + "\\Images\\" + displayArticle.Image.FileName;
                    FileName = uploadFileRepository.UplaodFile(displayArticle.Image, "\\Images\\");
                    article.Image = @"~/images/" + FileName;
                    System.IO.File.Delete(path_Old_Image);

                }
                else
                {
                    article.Image = displayArticle.ImagePath;
                }
                if ((displayArticle?.Video?.Length > 0) && (displayArticle?.Video?.ContentType == "video/mp4"))
                {
                    if(displayArticle.VideoPath==null)
                    {

                    }
                    else
                    {
                        var oldVideoPath = displayArticle.VideoPath.Remove(0, 8);
                        string path_Old_Image = path_root + "\\Video\\" + oldVideoPath;
                        System.IO.File.Delete(path_Old_Image);
                    }
                    FileName = uploadFileRepository.UplaodFile(displayArticle.Video, "\\Video\\");
                    article.Video = @"~/video/" + FileName;

                }
                if ((displayArticle?.PDF?.Length > 0) && (displayArticle?.PDF?.ContentType == "application/pdf"))
                {
                    if (displayArticle.PDFPath == null)
                    {

                    }
                    else
                    {
                        var oldPdfPath = displayArticle.PDFPath.Remove(0, 6);
                        string path_Old_Image = path_root + "\\Pdf\\" + oldPdfPath;
                        System.IO.File.Delete(path_Old_Image);


                    }

                    FileName = uploadFileRepository.UplaodFile(displayArticle.PDF, "\\Pdf\\");
                    article.PDF = @"~/pdf/" + FileName;

                }
                articleRepository.Update(article);
                    return RedirectToAction(nameof(ListArticle));
              

                
            }
            else
            {
                return View(article);
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
                category = categoryRepository.Get(article.CategoryId);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload2(IFormFile file, Article article)
        {
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Images\\Multi");
            int i = HttpContext.Request.Headers.Count;
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
            if (request == 2)
            {
                var fullPath = Path.Combine(uploads, file.FileName);

                System.IO.File.Delete(fullPath);


            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Upload2222(string name, int request)
        {
            var name2 = name;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file, Article article, string name, int request)
        {
            var request1 = request;
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Images\\Multi");
            int i = HttpContext.Request.Headers.Count;
            request = 1;
            if (request == 1)
            {
                if (file.Length > 0)
                {
                    Domain.Core.Images.Image image = new Domain.Core.Images.Image();
                    using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                    {
                        image.ArticleId = article.Id;
                        image.Name = @"~/images/" + file.FileName;
                        imageRepository.Add(image);
                        await file.CopyToAsync(fileStream);
                    }
                }
            }
            if (request == 2)
            {
                var fullPath = Path.Combine(uploads, file.FileName);

                System.IO.File.Delete(fullPath);


            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult UserList()
        {
            var userlist = userManager.Users.Take(50).OrderByDescending(x=>x.Id).ToList();
            return View(userlist);

        }
        [Authorize(Roles = "Admin")]
        public IActionResult CreateUser()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult UploadFile(IFormFile files)
        {
            string newFileName = uploadFileRepository.UplaodFile(files, "\\Images\\Profile\\");
            return Json(new { status = "success", message = "تصویر با موفقیت آپلود شد.", imagename = newFileName });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUser(UserViewModel model, string imagename)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (imagename == null)
                    {
                        model.UserImage = "DefaultProfile.jpg";
                    }
                    else
                    {
                        model.UserImage = imagename;
                    }

                    ApplicationUsers user = new ApplicationUsers
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        PhoneNumber = model.PhoneNumber,
                        UserName = model.UserName,
                        Email = model.Email,

                        Gender = model.gender,
                        BirthDate = model.BirthDayDate,
                        ProfileImage = model.UserImage
                    };

                    var result = userManager.CreateAsync(user, model.Password).Result;
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                }
                catch
                {
                    throw;
                }
            }
            ViewBag.ViewTitle = "فرم ایجاد کاربر";
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/Home");
        }
        [Authorize(Roles = "Admin")]

        public IActionResult Roles()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }
        [Authorize(Roles = "Admin")]

        public IActionResult EditRole(string Id)
        {
            var findrole = roleManager.FindByIdAsync(Id).Result;
            return View(findrole);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public IActionResult EditRole(string Id, string Name)
        {
            ApplicaionRoles findrole = roleManager.FindByIdAsync(Id).Result;
            findrole.Name = Name;
            var result = roleManager.UpdateAsync(findrole).Result;
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Roles));
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
            }
            return View(findrole);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult deleterole(string Id)
        {
            try
            {
                var findrole = roleManager.FindByIdAsync(Id).Result;
                var role = roleManager.DeleteAsync(findrole).Result;
                return Ok();
            }
            catch (Exception exp)
            {
                return BadRequest();
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Detailsrole(string Id)
        {
            ViewBag.type = 1;
            var findrole = roleManager.FindByIdAsync(Id).Result;

            if (findrole == null)
            {
                return BadRequest();
            }
            return PartialView("Detailsrole", findrole);

        }
        [Authorize(Roles = "Admin")]

        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddRole(ApplicaionRoles roles)
        {
            var result = roleManager.CreateAsync(roles).Result;
            return RedirectToAction(nameof(Roles));
        }
        public IActionResult RoleForUser(string Id)
        {
            UserRoleViewModel application = new UserRoleViewModel();
            application.User = userManager.FindByIdAsync(Id).Result;
            application.ApplicaionRoles = roleManager.Roles.ToList();
            return View(application);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public IActionResult RoleForUser(string Id, List<string> Roles)
        {

            UserRoleViewModel application = new UserRoleViewModel
            {
                User = userManager.FindByIdAsync(Id).Result
            };
            if (Roles.Count() != 0)
            {
                IList<string> RolesBelongUser;
                RolesBelongUser = userManager.GetRolesAsync(application.User).Result;
                var removeResult = userManager.RemoveFromRolesAsync(application.User, RolesBelongUser).Result;
                if (!removeResult.Succeeded)
                {
                    foreach (var item in removeResult.Errors)
                    {
                        ModelState.AddModelError(item.Code, item.Description);
                    }
                }
                var result = userManager.AddToRolesAsync(application.User, Roles).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(UserList));
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(item.Code, item.Description);
                    }
                }
            }
            else
            {
                application.ApplicaionRoles = roleManager.Roles.ToList();
            }
            return View(application);
        }
    }
}