﻿namespace ArtClubVT.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ArtClubVT.Data.Models;
    using ArtClubVT.Services.Mapping;

    public class EditItemViewModel : IMapFrom<Item>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MinLength(5)]
        public string Description { get; set; }

        [Required]
        public string Size { get; set; }

        [Required]
        public string Material { get; set; }

        [Required]
        [Range(0, 17976931348623157)]
        public double Price { get; set; }

        [Required]
        [Range(0, 17976931348623157)]
        public int Quantity { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Categories { get; set; }
    }
}
