using System.ComponentModel.DataAnnotations.Schema;

namespace ArtClubVT.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ArtClubVT.Data.Common.Models;

    public class Cart : BaseDeletableModel<int>
    {
        public Cart()
        {
            this.Items = new List<Item>();
        }

        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
