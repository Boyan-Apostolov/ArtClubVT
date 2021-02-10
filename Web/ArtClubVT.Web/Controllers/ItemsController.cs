namespace ArtClubVT.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
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

            await this.itemsService.AddItemToDbAsync(model);

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

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult EditItem(int id)
        {
            var viewModel = this.itemsService.GetItemById<EditItemViewModel>(id);
            viewModel.Categories = this.categoryService.GetAllAsKeyValuePairs();
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> EditItem(int id, EditItemViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.itemsService.EditItemAsync(id, input);

            return this.RedirectToAction(nameof(this.InfoById), new { id });
        }

        [Authorize]
        public IActionResult GetUserItems()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var viewModel = this.itemsService.GetUsersItems(userId);

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> DeleteItem(int id)
        {
            await this.itemsService.DeleteItemAsync(id);
            return this.RedirectToAction(nameof(this.GetAll));
        }

        [Authorize]
        public async Task<IActionResult> AddItemToUserCart(int itemId, int quantity)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.itemsService.AddItemToUserAsync(itemId, userId, quantity);
            return this.RedirectToAction(nameof(this.GetUserItems));
        }

        [Authorize]
        public async Task<IActionResult> RemoveItemFromUserItems(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.itemsService.RemoveItemFromUserItems(id, userId);
            return this.RedirectToAction(nameof(this.GetUserItems));
        }
    }
}
