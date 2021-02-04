namespace ArtClubVT.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ArtClubVT.Data.Common.Models;

    public class CategoryItem : BaseDeletableModel<int>
    {
        public Item Item { get; set; }

        public int ItemId { get; set; }

        public Category Category { get; set; }

        public int CategoryId { get; set; }
    }
}
