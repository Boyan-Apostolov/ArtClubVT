namespace ArtClubVT.Web.Controllers
{
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
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            model.UserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var orderId = await this.ordersService.CreateOrderAsync(model);
            return this.RedirectToAction(nameof(this.SuccessfulOrder), new { orderId });
        }

        [Authorize]
        public IActionResult SuccessfulOrder(int orderId)
        {
            this.ViewBag.OrderId = orderId;
            return this.View();
        }
    }
}
