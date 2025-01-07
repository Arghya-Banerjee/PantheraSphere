using Core;
using Microsoft.AspNetCore.Mvc;
using PantheraSphere.Models;

namespace PantheraSphere.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("LoginValidate")]
        public IActionResult LoginValidate(string username, string password)
        {
            LoginModel loginDetails = DBOperations<LoginModel>.GetSpecific(new LoginModel
            {
                OpMode = 0, // Validate User
                UserName = username,
                PassCode = password
            }, Constant.usp_Login);

            System.Diagnostics.Debug.WriteLine(username + " Hola " + password);

            if (loginDetails.UserName == null)
            {
                return Json(new { success = false, message = "User doesn't exist" });
            }

            return Ok();
        }
    }
}
