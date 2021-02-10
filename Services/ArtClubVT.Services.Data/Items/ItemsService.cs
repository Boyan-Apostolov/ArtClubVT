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
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<ItemsUsers> itemsUsersRepository;

        public ItemsService(
            IDeletableEntityRepository<Item> itemsRepository,
            IConfiguration configuration,
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<ItemsUsers> itemsUsersRepository)
        {
            this.itemsRepository = itemsRepository;
            this.configuration = configuration;
            this.usersRepository = usersRepository;
            this.itemsUsersRepository = itemsUsersRepository;
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

        public ICollection<ItemsUsers> GetUsersItems(string userId)
        {
            return this.itemsUsersRepository.All()
                .Include(x => x.Item)
                .Where(x => x.ApplicationUserId == userId)
                .ToList();
        }

        public async Task AddItemToUserAsync(int itemId, string userId, int quantity = 1)
        {
            var user = this.usersRepository.All()
                .FirstOrDefault(x => x.Id == userId);

            var item = this.itemsRepository.All().FirstOrDefault(x => x.Id == itemId);
            if (item.Quantity >= quantity)
            {
                user.Items.Add(new ItemsUsers()
                {
                    ApplicationUserId = userId,
                    ApplicationUser = user,
                    ItemId = itemId,
                    Item = item,
                    Quantity = quantity,
                });
                await this.usersRepository.SaveChangesAsync();
            }
        }

        public async Task RemoveItemFromUserItems(int id, string userId)
        {
            var user = this.usersRepository.All()
                .Include(x => x.Items)
                .FirstOrDefault(x => x.Id == userId);

            var userItem = user.Items.FirstOrDefault(x => x.ItemId == id);
            user.Items.Remove(userItem);

            await this.usersRepository.SaveChangesAsync();
        }
    }
}
