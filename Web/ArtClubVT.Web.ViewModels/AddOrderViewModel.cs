﻿namespace ArtClubVT.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class AddOrderViewModel
    {
        public int ItemId { get; set; }

        public string UserId { get; set; }

        public int Quantity { get; set; }

        [Required]
        [MinLength(5)]
        public string BuyerName { get; set; }

        [Required]
        [MinLength(5)]
        public string BuyerPhone { get; set; }

        [Required]
        [MinLength(5)]
        public string BuyerAddress { get; set; }

        public string Note { get; set; }
    }
}
