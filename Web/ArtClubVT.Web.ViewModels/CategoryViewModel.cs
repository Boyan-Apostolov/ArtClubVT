namespace ArtClubVT.Web.ViewModels
{
    using ArtClubVT.Data.Models;
    using ArtClubVT.Services.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
