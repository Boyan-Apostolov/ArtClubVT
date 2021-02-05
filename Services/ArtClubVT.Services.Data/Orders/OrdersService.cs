using ArtClubVT.Services.Data.Items;

namespace ArtClubVT.Services.Data.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using ArtClubVT.Data.Common.Repositories;
    using ArtClubVT.Data.Models;
    using ArtClubVT.Web.ViewModels;

    public class OrdersService : IOrdersService
    {
        private readonly IDeletableEntityRepository<Order> ordersRepository;
        private readonly IItemsService itemsService;

        public OrdersService(IDeletableEntityRepository<Order> ordersRepository, IItemsService itemsService)
        {
            this.ordersRepository = ordersRepository;
            this.itemsService = itemsService;
        }

        public async Task<int> CreateOrder(AddOrderViewModel model)
        {
            var order = new Order()
            {
                Item = this.itemsService.GetItemById(model.ItemId),
                ApplicationUserId = model.UserId,
                BuyerName = model.BuyerName,
                BuyerAddress = model.BuyerAddress,
                BuyerPhone = model.BuyerPhone,
                Note = model.Note == null ? model.Note : "няма",
            };
            await this.ordersRepository.AddAsync(order);
            await this.ordersRepository.SaveChangesAsync();
            return order.Id;
        }
    }
}
