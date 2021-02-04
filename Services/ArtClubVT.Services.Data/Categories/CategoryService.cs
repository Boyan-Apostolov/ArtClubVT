using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtClubVT.Data.Common.Repositories;
using ArtClubVT.Data.Models;

namespace ArtClubVT.Services.Data.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoryService(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task AddCategoryToDb(string name)
        {
            await this.categoryRepository.AddAsync(new Category() {Name = name});
            await this.categoryRepository.SaveChangesAsync();
        }

        public ICollection<Category> GetAllCategories()
        {
            return this.categoryRepository.All().ToList();
        }
    }
}
