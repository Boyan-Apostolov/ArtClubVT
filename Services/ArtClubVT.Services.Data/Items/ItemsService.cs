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
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public class ItemsService : IItemsService
    {
        private readonly IDeletableEntityRepository<Item> itemsRepository;
        private readonly IConfiguration configuration;

        public ItemsService(IDeletableEntityRepository<Item> itemsRepository, IConfiguration configuration)
        {
            this.itemsRepository = itemsRepository;
            this.configuration = configuration;
        }

        public ICollection<T> GetItemsByCategory<T>(string categoryName)
        {
            return this.itemsRepository.All()
                .Where(x => x.Category.Name == categoryName && x.Quantity >= 1)
                .To<T>()
                .ToList();
        }

        public ICollection<T> GetAll<T>()
        {
            return this.itemsRepository.All()
                .Where(x => x.Quantity >= 1)
                .To<T>()
                .ToList();
        }

        public async Task AddItemToDbAsync(CreateItemViewModel model)
        {
            var imageUrl = await this.UploadItemPictureAsync(model.Image);

            var item = new Item()
            {
                Name = model.Name,
                Description = model.Description,
                Size = model.Size,
                Material = model.Material,
                ImageLink = imageUrl,
                Price = model.Price,
                Quantity = model.Quantity,
                CategoryId = model.CategoryId,
            };
            await this.itemsRepository.AddAsync(item);

            await this.itemsRepository.SaveChangesAsync();
        }

        public async Task<string> UploadItemPictureAsync(IFormFile file)
        {
            var cloudinaryAccount = this.configuration.GetSection("Cloudinary");
            Account account = new Account(
                cloudinaryAccount["Cloud_Name"],
                cloudinaryAccount["API_Key"],
                cloudinaryAccount["API_Secret"]);

            var cloudinary = new Cloudinary(account);

            var uploadResult = new ImageUploadResult();

            var imageUrl = string.Empty;

            if (file != null)
            {
                if (file.Length > 0)
                {
                    await using var stream = file.OpenReadStream();
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(256).Height(256).Gravity("face").Radius("max").Border("2px_solid_white").Crop("thumb"),
                    };

                    uploadResult = cloudinary.Upload(uploadParams);
                }

                imageUrl = uploadResult.Url.ToString();
            }

            return imageUrl;
        }

        public T GetItemById<T>(int id)
        {
            return this.itemsRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();
        }

        public Item GetItemById(int id)
        {
            return this.itemsRepository.All().FirstOrDefault(x => x.Id == id);
        }

        public async Task EditItemAsync(int id, EditItemViewModel model)
        {
            var item = this.GetItemById(id);

            item.Name = model.Name;
            item.Description = model.Description;
            item.Price = model.Price;
            item.CategoryId = model.CategoryId;
            item.Material = model.Material;
            item.Quantity = model.Quantity;
            item.Size = model.Size;

            await this.itemsRepository.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(int id)
        {
            var item = await this.itemsRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            this.itemsRepository.Delete(item);
            await this.itemsRepository.SaveChangesAsync();
        }
    }
}
