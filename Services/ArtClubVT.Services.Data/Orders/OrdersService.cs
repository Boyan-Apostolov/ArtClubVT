namespace ArtClubVT.Services.Data.Orders
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ArtClubVT.Data.Common.Repositories;
    using ArtClubVT.Data.Models;
    using ArtClubVT.Services.Data.Emails;
    using ArtClubVT.Services.Data.Items;
    using ArtClubVT.Services.Mapping;
    using ArtClubVT.Web.ViewModels;
    using Microsoft.EntityFrameworkCore;

    public class OrdersService : IOrdersService
    {
        private readonly IDeletableEntityRepository<Order> ordersRepository;
        private readonly IItemsService itemsService;
        private readonly IEmailsService emailsService;
        private readonly IRepository<ApplicationUser> usersRepository;

        public OrdersService(
            IDeletableEntityRepository<Order> ordersRepository,
            IItemsService itemsService,
            IEmailsService emailsService,
            IRepository<ApplicationUser> usersRepository)
        {
            this.ordersRepository = ordersRepository;
            this.itemsService = itemsService;
            this.emailsService = emailsService;
            this.usersRepository = usersRepository;
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
                IsConfirmed = null,
            };

            var userMail = this.usersRepository.All().FirstOrDefault(x => x.Id == model.UserId).Email;
            await this.emailsService.NotifyAdminForOrder(userMail);

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

        public ICollection<T> GetUserOrders<T>(string userId)
        {
            return this.ordersRepository.All()
                .Where(x => x.ApplicationUserId == userId)
                .To<T>().ToList();
        }

        public ICollection<T> GetAllOrders<T>()
        {
            return this.ordersRepository.All()
                .Include(x => x.ApplicationUser)
                .To<T>().ToList();
        }

        public T GetOrderInfo<T>(int orderId)
        {
            return this.ordersRepository.All()
                .Where(x => x.Id == orderId).To<T>().FirstOrDefault();
        }

        public async Task ApproveOrderAsync(int orderId)
        {
            var order = this.ordersRepository.All()
                .Include(x => x.ApplicationUser)
                .FirstOrDefault(x => x.Id == orderId);
            order.IsConfirmed = true;

            await this.emailsService.ApproveOrderEmail(order.ApplicationUser.Email);
            await this.ordersRepository.SaveChangesAsync();
        }

        public async Task DeclineOrderAsync(int orderId)
        {
            var order = this.ordersRepository.All()
                .Include(x => x.ApplicationUser)
                .FirstOrDefault(x => x.Id == orderId);
            order.IsConfirmed = false;

            await this.emailsService.DeclineOrderEmail(order.ApplicationUser.Email);
            await this.ordersRepository.SaveChangesAsync();
        }
    }
}
