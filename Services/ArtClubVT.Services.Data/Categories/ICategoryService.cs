namespace ArtClubVT.Services.Data.Categories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        public Task AddCategoryToDbAsync(string name);

        public ICollection<T> GetAllCategories<T>();

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
