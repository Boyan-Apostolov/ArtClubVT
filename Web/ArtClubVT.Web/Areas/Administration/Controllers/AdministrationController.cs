namespace ArtClubVT.Web.Areas.Administration.Controllers
{
    using ArtClubVT.Common;
    using ArtClubVT.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
