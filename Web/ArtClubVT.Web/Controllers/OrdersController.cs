namespace ArtClubVT.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using ArtClubVT.Services.Data.Orders;
    using ArtClubVT.Web.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class OrdersController : Controller
    {
        private readonly IOrdersService ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        [Authorize]
        public IActionResult OrderItem(int id)
        {
            var viewModel = new AddOrderViewModel() { ItemId = id };
            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> OrderItem(AddOrderViewModel model)
        {
            model.UserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var orderId = await this.ordersService.CreateOrder(model);
            return this.RedirectToAction(nameof(this.SuccessfullOrder), new {orderId});
        }

        [Authorize]
        public IActionResult SuccessfullOrder(int orderId)
        {
            this.ViewBag.OrderId = orderId;
            return this.View();
        }
    }
}
