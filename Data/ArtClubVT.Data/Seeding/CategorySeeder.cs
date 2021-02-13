namespace ArtClubVT.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ArtClubVT.Data.Models;

    public class CategorySeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var categoryList = new List<Category>()
            {
                new Category() { Name = "Абстрактни"},
                new Category() { Name ="Стикери"},
                new Category() { Name ="Скици"},
                new Category() { Name ="За подарък"},
                new Category() { Name ="Смешки"},
                new Category() { Name ="За деца"},
                new Category() { Name ="За възрастни"},
            };

            await dbContext.Categories.AddRangeAsync(categoryList);
        }
    }
}
