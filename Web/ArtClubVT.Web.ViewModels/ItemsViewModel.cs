namespace ArtClubVT.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ArtClubVT.Data.Models;
    using ArtClubVT.Services.Mapping;

    public class ItemsViewModel
    {
        public ICollection<ItemViewModel> Items { get; set; }
    }
}
