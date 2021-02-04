using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtClubVT.Data.Common.Repositories;
using ArtClubVT.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtClubVT.Services.Data.Items
{
    public class ItemsService : IItemsService
    {
        private readonly IDeletableEntityRepository<Item> itemsRepository;

        public ItemsService(IDeletableEntityRepository<Item> itemsRepository)
        {
            this.itemsRepository = itemsRepository;
        }

        public ICollection<Item> GetItemsByCategory(string categoryName)
        {
            return this.itemsRepository.All()
                .Include(x => x.Categories)
                .Where(x => x.Categories
                    .Any(n => n.Category.Name == categoryName))
                .ToList();
        }
    }
}
