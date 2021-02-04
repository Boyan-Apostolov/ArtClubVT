namespace ArtClubVT.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class ItemsController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult GetItemsByCategoryName(string categoryName)
        {
            return null; // TODO: Get items from items
        }
    }
}
