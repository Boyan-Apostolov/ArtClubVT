using ArtClubVT.Services.Data.Carts;

namespace ArtClubVT.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class CartsController : Controller
    {
        private readonly ICartsService cartsService;

        public CartsController(ICartsService cartsService)
        {
            this.cartsService = cartsService;
        }

        public IActionResult GetUsersCart()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //var viewModel = this.cartsService.GetUsersCart<UserCartViewModel>(userId); // TODO : Create UserCartViewModel
            return this.View();
        }

        public async Task<IActionResult> AddItemToUserCart(int itemId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.cartsService.AddItemToCartAsync(itemId, userId);
            return this.RedirectToAction(nameof(GetUsersCart));
        }
    }
}
