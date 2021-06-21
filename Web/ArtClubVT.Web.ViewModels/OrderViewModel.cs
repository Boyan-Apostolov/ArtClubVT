namespace ArtClubVT.Web.ViewModels
{
    using System;

    using ArtClubVT.Data.Models;
    using ArtClubVT.Services.Mapping;

    public class OrderViewModel : IMapFrom<Order>
    {
        public int Id { get; set; }

        public string ItemName { get; set; }

        public string Note { get; set; }

        public int Quantity { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsCompleted { get; set; }

        public bool? IsConfirmed { get; set; }

        public string ApplicationUserEmail { get; set; }

        public string ItemImageLink { get; set; }
    }
}
