﻿namespace ArtClubVT.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ArtClubVT.Data.Common.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
