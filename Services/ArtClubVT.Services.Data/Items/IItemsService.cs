namespace ArtClubVT.Services.Data.Items
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using ArtClubVT.Data.Models;
    using ArtClubVT.Web.ViewModels;

    public interface IItemsService
    {
        public ICollection<T> GetItemsByCategory<T>(string categoryName);

        public ICollection<T> GetAll<T>();

        public Task AddItemToDb(CreateItemViewModel model);
    }
}
