namespace ArtClubVT.Services.Data.Categories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        public Task AddCategoryToDb(string name);

        public ICollection<T> GetAllCategories<T>();
    }
}
