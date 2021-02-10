namespace ArtClubVT.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ArtClubVT.Data.Models;
    using ArtClubVT.Services.Mapping;

    public class OrderViewModel : IMapFrom<Order>
    {
        public string ItemName { get; set; }

        public string Note { get; set; }

        public int Quantity { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
