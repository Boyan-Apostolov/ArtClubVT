namespace ArtClubVT.Services.Data.Items
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ArtClubVT.Data.Models;
    using ArtClubVT.Web.ViewModels;
    using Microsoft.AspNetCore.Http;

    public interface IItemsService
    {
        public ICollection<T> GetItemsByCategory<T>(string categoryName);

        public ICollection<T> GetAll<T>();

        public Task AddItemToDbAsync(CreateItemViewModel model);

        public Task<string> UploadItemPictureAsync(IFormFile file);

        public T GetItemById<T>(int id);

        public Item GetItemById(int id);

        public Task EditItemAsync(int id, EditItemViewModel model);

        public Task DeleteItemAsync(int id);

        public ICollection<ItemsUsers> GetUsersItems(string userId);

        Task AddItemToUserAsync(int itemId, string userId, int quantity = 1);

        Task RemoveItemFromUserItems(int id, string userId);
    }
}
