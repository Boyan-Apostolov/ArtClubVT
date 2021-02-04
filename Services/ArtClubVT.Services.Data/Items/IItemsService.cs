namespace ArtClubVT.Services.Data.Items
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ArtClubVT.Data.Models;

    public interface IItemsService
    {
        public ICollection<T> GetItemsByCategory<T>(string categoryName);
    }
}
