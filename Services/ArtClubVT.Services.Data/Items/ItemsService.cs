namespace ArtClubVT.Services.Data.Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using ArtClubVT.Data.Common.Repositories;
    using ArtClubVT.Data.Models;
    using ArtClubVT.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class ItemsService : IItemsService
    {
        private readonly IDeletableEntityRepository<Item> itemsRepository;

        public ItemsService(IDeletableEntityRepository<Item> itemsRepository)
        {
            this.itemsRepository = itemsRepository;
        }

        public ICollection<T> GetItemsByCategory<T>(string categoryName)
        {
            return this.itemsRepository.All()
                .Include(x => x.Categories)
                .Where(x => x.Categories
                    .Any(n => n.Category.Name == categoryName))
                .To<T>()
                .ToList();
        }
    }
}
