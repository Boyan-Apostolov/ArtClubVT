namespace ArtClubVT.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ArtClubVT.Data.Models;
    using ArtClubVT.Services.Mapping;
    using AutoMapper;

    public class ItemViewModel : IMapFrom<Item>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Size { get; set; }

        public string Material { get; set; }

        public string ImageLink { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ItemViewModel, ItemsUsers>()
                .ForMember(
                    x => x.Quantity,
                    c => c.MapFrom(e => e.Quantity));
        }
    }
}
