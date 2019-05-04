using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpDesk.Domain.Contracts.Categories;
using HelpDesk.Domain.Core.Categories;
using HelpDesk.InfraStructures.DataAccess.Common;
using HelpDesk.MVC.Models.categories;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        public AdminController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
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
            return View(cat);
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
        public ActionResult Details(int Id)
        {
            ViewBag.type = 1;
            var cat = categoryRepository.Get(Id);
            if (cat == null)
            {
                return BadRequest();
            }
            return PartialView("Details", cat);

        }
        public ActionResult Edit(int Id)
        {
            var cat = categoryRepository.Get(Id);
            if (cat == null)
            {
                return BadRequest();
            }
            return View(cat);

        }
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            categoryRepository.Update(category);
           return RedirectToAction(nameof(ListCat));
            

        }


    }
}