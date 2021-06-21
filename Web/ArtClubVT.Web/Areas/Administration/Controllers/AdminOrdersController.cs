namespace ArtClubVT.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ArtClubVT.Services.Data.Orders;
    using ArtClubVT.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class AdminOrdersController : AdministrationController
    {
        private readonly IOrdersService ordersService;

        public AdminOrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        public IActionResult Index()
        {
            var viewModel = this.ordersService.GetAllOrders<OrderViewModel>();
            return this.View(viewModel);
        }

        public IActionResult GetUserOrder(int id)
        {
            var viewModel = this.ordersService.GetOrderAdministration<AdminOrderViewModel>(id);
            return this.View(viewModel);
        }
    }
}
