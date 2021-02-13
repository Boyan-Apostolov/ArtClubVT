namespace ArtClubVT.Data.Models
{
    using System.Collections.Generic;

    using ArtClubVT.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Items = new HashSet<Item>();
        }

        public string Name { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
