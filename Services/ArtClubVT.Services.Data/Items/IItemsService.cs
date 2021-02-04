using System;
using System.Collections.Generic;
using System.Text;
using ArtClubVT.Data.Models;

namespace ArtClubVT.Services.Data.Items
{
    public interface IItemsService
    {
        public ICollection<Item> GetItemsByCategory(string categoryName);
    }
}
