namespace ArtClubVT.Services.Data.Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using ArtClubVT.Data.Common.Repositories;
    using ArtClubVT.Data.Models;
    using ArtClubVT.Services.Mapping;
    using ArtClubVT.Web.ViewModels;
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

        public ICollection<T> GetAll<T>()
        {
            return this.itemsRepository.All()
                .To<T>()
                .ToList();
        }

        public async Task AddItemToDb(CreateItemViewModel model)
        {
            var item = new Item()
            {
                Name = model.Name,
                Description = model.Description,
                Size = model.Size,
                Material = model.Material,
                ImageLink = "No image yet", // TODO: Work with images
                Price = model.Price,
                Quantity = model.Quantity,
            };
            await this.itemsRepository.AddAsync(item);

            item.Categories.Add(new CategoryItem()
            { CategoryId = model.CategoryId, ItemId = item.Id });

            await this.itemsRepository.SaveChangesAsync();
        }
    }
}
