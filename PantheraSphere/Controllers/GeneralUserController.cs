using Microsoft.AspNetCore.Mvc;
using PantheraSphere.Models;

namespace PantheraSphere.Controllers
{
    [Route("[controller]")]
    public class GeneralUserController : Controller
    {
        [Route("Dashboard")]
        public IActionResult Dashboard()
        {
            UserSec sessionDetails = HttpContext.Session.GetObjectFromJson<UserSec>("SessionDetails");
            if (sessionDetails == null || sessionDetails.RoleID != 4)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.UniqueTigers = 10;
            ViewBag.ChecklistsSubmitted = 7;
            ViewBag.UniqueLocations = 13;
            string username = HttpContext.Session.GetObjectFromJson<UserSec>("SessionDetails").UserName;
            ViewBag.UserName = username;
            return View();
        }
    }
}
