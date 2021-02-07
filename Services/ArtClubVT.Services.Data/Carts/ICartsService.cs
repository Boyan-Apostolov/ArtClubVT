namespace ArtClubVT.Services.Data.Carts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using ArtClubVT.Data.Models;

    public interface ICartsService
    {
        public ICollection<T> GetUsersCart<T>(string userId);

        public Task AddItemToCartAsync(int itemId, string userId);

        public Cart GetCartByUserId(string userId);

        public Task<Cart> GetOrCreateCartByUserId(string userId);
    }
}
