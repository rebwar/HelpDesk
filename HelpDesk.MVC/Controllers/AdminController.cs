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
            return RedirectToAction(nameof(Index));
        }
        public IActionResult ListCat()
        {
              
            return View(categoryRepository.GetAll().ToList());
        }
        public IActionResult EditCategory(int id)
        {
            return View();
        }
        public IActionResult DeleteCategory(int id)
        {
            return View();
        }
    }
}