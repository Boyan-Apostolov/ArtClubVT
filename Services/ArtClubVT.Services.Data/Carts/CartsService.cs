using ArtClubVT.Services.Data.Items;
using AutoMapper.Configuration.Annotations;

namespace ArtClubVT.Services.Data.Carts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using ArtClubVT.Data.Common.Repositories;
    using ArtClubVT.Data.Models;
    using ArtClubVT.Services.Mapping;

    public class CartsService : ICartsService
    {
        private readonly IDeletableEntityRepository<Cart> cartsRepository;
        private readonly IItemsService itemsService;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public CartsService(IDeletableEntityRepository<Cart> cartsRepository, IItemsService itemsService, IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.cartsRepository = cartsRepository;
            this.itemsService = itemsService;
            this.usersRepository = usersRepository;
        }

        public ICollection<T> GetUsersCart<T>(string userId)
        {
            return this.cartsRepository.All()
                .Where(x => x.ApplicationUserId == userId)
                .To<T>()
                .ToList();
        }

        public async Task AddItemToCartAsync(int itemId, string userId)
        {
            var cart = await this.GetOrCreateCartByUserId(userId);
            var item = this.itemsService.GetItemById(itemId); //TODO: Implement
        }

        public Cart GetCartByUserId(string userId)
        {
            return this.cartsRepository.All()
                .FirstOrDefault(x => x.ApplicationUserId == userId);
        }

        public async Task<Cart> GetOrCreateCartByUserId(string userId)
        {
            var user = this.usersRepository.All().FirstOrDefault(x => x.Id == userId);

            var cart = this.GetCartByUserId(userId);

            if (cart == null)
            {
                cart = new Cart() { ApplicationUser = user };
                await this.cartsRepository.AddAsync(cart);
                await this.cartsRepository.SaveChangesAsync();
            }

            return cart;
        }
    }
}
