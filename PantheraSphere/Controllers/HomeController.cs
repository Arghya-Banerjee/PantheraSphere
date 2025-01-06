using Microsoft.AspNetCore.Mvc;

namespace PantheraSphere.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [Route("~/")]
        [Route("Home")]
        public IActionResult Home()
        {
            return View();
        }
    }
}
