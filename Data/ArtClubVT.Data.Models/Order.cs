namespace ArtClubVT.Data.Models
{
    using ArtClubVT.Data.Common.Models;

    public class Order : BaseDeletableModel<int>
    {
        public int ItemId { get; set; }

        public Item Item { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string BuyerName { get; set; }

        public string BuyerPhone { get; set; }

        public string BuyerAddress { get; set; }

        public string Note { get; set; }

        public int Quantity { get; set; }

        public bool IsCompleted { get; set; }

        public bool? IsConfirmed { get; set; }
    }
}
