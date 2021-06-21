namespace ArtClubVT.Web.ViewModels
{
    using System;

    using ArtClubVT.Data.Models;
    using ArtClubVT.Services.Mapping;

    public class AdminOrderViewModel : IMapFrom<Order>
    {
        public int Id { get; set; }

        public Item Item { get; set; }

        public string Note { get; set; }

        public int Quantity { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsCompleted { get; set; }

        public bool? IsConfirmed { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string BuyerName { get; set; }

        public string BuyerPhone { get; set; }

        public string BuyerAddress { get; set; }

        public string ItemImageLink { get; set; }
    }
}
