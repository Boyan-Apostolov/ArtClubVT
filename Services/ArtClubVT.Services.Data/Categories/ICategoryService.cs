using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ArtClubVT.Data.Models;

namespace ArtClubVT.Services.Data.Categories
{
    public interface ICategoryService
    {
        public Task AddCategoryToDb(string name);

        public ICollection<Category> GetAllCategories();

    }
}
