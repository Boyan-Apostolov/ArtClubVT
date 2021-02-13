namespace ArtClubVT.Data.Models
{
    using ArtClubVT.Data.Common.Models;

    public class ItemsUsers : BaseDeletableModel<int>
    {
        public ApplicationUser ApplicationUser { get; set; }

        public string ApplicationUserId { get; set; }

        public Item Item { get; set; }

        public int ItemId { get; set; }

        public int Quantity { get; set; }
    }
}
