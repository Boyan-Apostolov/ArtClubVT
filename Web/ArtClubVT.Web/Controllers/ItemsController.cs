using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtClubVT.Web.Controllers
{
    public class ItemsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetItemsByCategoryName(string categoryName)
        {
            return null; //TODO: Get items from items
        }
    }
}
