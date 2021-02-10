namespace ArtClubVT.Services.Data.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using ArtClubVT.Data.Common.Repositories;
    using ArtClubVT.Data.Models;
    using ArtClubVT.Services.Data.Items;
    using ArtClubVT.Web.ViewModels;

    public class OrdersService : IOrdersService
    {
        private readonly IDeletableEntityRepository<Order> ordersRepository;
        private readonly IItemsService itemsService;

        public OrdersService(
            IDeletableEntityRepository<Order> ordersRepository,
            IItemsService itemsService)
        {
            this.ordersRepository = ordersRepository;
            this.itemsService = itemsService;
        }

        public async Task<int> CreateOrderAsync(AddOrderViewModel model)
        {
            var item = this.itemsService.GetItemById(model.ItemId);
            var order = new Order()
            {
                Item = item,
                ApplicationUserId = model.UserId,
                BuyerName = model.BuyerName,
                BuyerAddress = model.BuyerAddress,
                BuyerPhone = model.BuyerPhone,
                Note = model.Note == null ? model.Note : "няма",
                Quantity = model.Quantity,
            };

            item.Quantity = (item.Quantity - model.Quantity) <= 0 ? 0 : (item.Quantity - model.Quantity);
            await this.ordersRepository.AddAsync(order);
            await this.ordersRepository.SaveChangesAsync();
            return order.Id;
        }

        public async Task BuyEverythingFromUserItems(AddOrderViewModel model)
        {
            var userItems = this.itemsService.GetUsersItems(model.UserId);
            foreach (var item in userItems)
            {
                model.ItemId = item.ItemId;
                model.Quantity = item.Quantity;

                await this.CreateOrderAsync(model);
                await this.itemsService.RemoveItemFromUserItems(item.ItemId, model.UserId);
            }
        }
    }
}
