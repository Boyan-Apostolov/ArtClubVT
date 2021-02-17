namespace ArtClubVT.Services.Data.Orders
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ArtClubVT.Web.ViewModels;

    public interface IOrdersService
    {
        Task<int> CreateOrderAsync(AddOrderViewModel model);

        Task BuyEverythingFromUserItems(AddOrderViewModel model);

        ICollection<T> GetUserOrders<T>(string userId);

        ICollection<T> GetAllOrders<T>();

        T GetOrderInfo<T>(int orderId);

        Task ApproveOrderAsync(int orderId);

        Task DeclineOrderAsync(int orderId);
    }
}
