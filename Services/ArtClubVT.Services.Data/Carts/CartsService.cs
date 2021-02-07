using System.Security.Cryptography.X509Certificates;
using ArtClubVT.Services.Data.Items;
using AutoMapper.Configuration.Annotations;
using Microsoft.EntityFrameworkCore;

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
            //TODO : Move to items service
            return null;
        }

        public async Task<int> AddItemToCartAsync(int itemId, string userId)
        {
            // TODO: Implement correctly
            var user = this.usersRepository.All().FirstOrDefault(x => x.Id == userId);
            user.Items.Add(new ItemsUsers()
            {
                ApplicationUserId = userId,
                ItemId = itemId,
                Quantity = 1,
            });
            await this.usersRepository.SaveChangesAsync();
            return user.Items.Count;
        }

        public Cart GetCartByUserId(string userId)
        {
            //TODO : Move to items service
            return null;
            //return this.cartsRepository.All()
            //    .FirstOrDefault(x => x.ApplicationUserId == userId);
        }

        public async Task<Cart> GetOrCreateCartByUserId(string userId)
        {
            //TODO : remove
            //var user = this.usersRepository.All().FirstOrDefault(x => x.Id == userId);

            //var cart = 1;//.user.Cart;
            //cart.ApplicationUser = user;
            //await this.cartsRepository.SaveChangesAsync();
            //await this.usersRepository.SaveChangesAsync();

            return null;
        }
    }
}
