namespace ArtClubVT.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ArtClubVT.Data.Common.Models;

    public class Item : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Size { get; set; }

        public string Material { get; set; }

        public string ImageLink { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public Category Category { get; set; }

        public int CategoryId { get; set; }
    }
}
