using System;
using System.Collections.Generic;
using System.Text;
using ArtClubVT.Data.Common.Models;

namespace ArtClubVT.Data.Models
{
    public class Item : BaseDeletableModel<int>
    {
        public Item()
        {
            this.Categories = new HashSet<CategoryItem>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Size { get; set; }

        public string Material { get; set; }

        public int ProductCode { get; set; }

        public string ImageLink { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public ICollection<CategoryItem> Categories { get; set; }
    }
}
