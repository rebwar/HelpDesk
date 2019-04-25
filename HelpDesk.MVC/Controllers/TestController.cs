using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpDesk.Domain.Contracts.Categories;
using HelpDesk.Domain.Core.Categories;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.MVC.Controllers
{
    public class TestController : Controller
    {
        private readonly ICategoryRepository categoryRepository;

        public TestController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
           
          
            return View();
        }
        public List<Category> SearchCategory(string search)
        {
            return categoryRepository.SearchCategory(search).ToList();
        }
    }
}