namespace ArtClubVT.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ArtClubVT.Common;
    using ArtClubVT.Services.Data.Categories;
    using ArtClubVT.Services.Data.Items;
    using ArtClubVT.Web.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class ItemsController : Controller
    {
        private readonly IItemsService itemsService;
        private readonly ICategoryService categoryService;

        public ItemsController(
            IItemsService itemsService,
            ICategoryService categoryService)
        {
            this.itemsService = itemsService;
            this.categoryService = categoryService;
        }

        public IActionResult GetAll()
        {
            var items = this.itemsService.GetAll<ItemViewModel>();
            return this.View(items);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult AddItem()
        {
            var viewModel = new CreateItemViewModel();
            viewModel.Categories = this.categoryService.GetAllAsKeyValuePairs();
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> AddItem(CreateItemViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Categories = this.categoryService.GetAllAsKeyValuePairs();
                return this.View(model);
            }

            await this.itemsService.AddItemToDb(model);

            return this.RedirectToAction(nameof(this.GetAll));
        }

        public IActionResult GetItemsByCategoryName(string categoryName)
        {
            var items = this.itemsService.GetItemsByCategory<ItemViewModel>(categoryName);
            this.ViewBag.CategoryName = categoryName;
            return this.View(items);
        }

        public IActionResult InfoById(int id)
        {
            var item = this.itemsService.GetItemById<ItemViewModel>(id);
            return this.View(item);
        }
    }
}
