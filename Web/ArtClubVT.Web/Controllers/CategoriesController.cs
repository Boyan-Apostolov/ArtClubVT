using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtClubVT.Services.Data.Categories;

namespace ArtClubVT.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public IActionResult GetAll()
        {
            var categories = this.categoryService.GetAllCategories();
            return this.View(categories);
        }

        public IActionResult AddCategory()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(string name)
        {
            await this.categoryService.AddCategoryToDb(name);
            return this.RedirectToAction(nameof(this.GetAll));
        }
    }
}
