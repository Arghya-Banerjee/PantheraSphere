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

        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
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
            else
            {
                if (username == loginDetails.UserName && password == loginDetails.PassCode)
                {
                    UserSec sessionDetails = DBOperations<UserSec>.GetSpecific(new UserSec
                    {
                        OpMode = 0,
                        UserName = username,
                    }, Constant.usp_UserSec);

                    System.Diagnostics.Debug.WriteLine(sessionDetails.UserName + " " + sessionDetails.RoleName + " " + sessionDetails.RoleID);

                    if (sessionDetails.RoleID == 1) // Admin
                    {
                        HttpContext.Session.SetObjectAsJson("SessionDetails", sessionDetails);
                        return Json(new { success = true, redirectUrl = Url.Action("Dashboard", "Admin") });
                    }
                    else if (sessionDetails.RoleID == 2) // Researcher
                    {
                        HttpContext.Session.SetObjectAsJson("SessionDetails", sessionDetails);
                        return Json(new { success = true, redirectUrl = Url.Action("Dashboard", "Researcher") });
                    }
                    else if (sessionDetails.RoleID == 3) // Conservationist
                    {
                        HttpContext.Session.SetObjectAsJson("SessionDetails", sessionDetails);
                        return Json(new { success = true, redirectUrl = Url.Action("Dashboard", "Conservationist") });
                    }
                    else if (sessionDetails.RoleID == 4) // General User
                    {
                        HttpContext.Session.SetObjectAsJson("SessionDetails", sessionDetails);
                        return Json(new { success = true, redirectUrl = Url.Action("Dashboard", "GeneralUser") });
                    }
                }
            }

            return Json(new { success = false, message = "Unknown Error" });
        }

        [HttpPost("RegisterValidate")]
        public IActionResult RegisterValidate(string username, string email, long phone, string password, string confirmPassword)
        {
            RegisterModel regDetails = DBOperations<RegisterModel>.GetSpecific(new RegisterModel
            {
                OpMode = 0,
                UserName = username
            }, Constant.usp_Register);

            if(regDetails.UserName == username)
            {
                return Json(new { success = false, message = "User already exists"});
            }
            else
            {
                DBOperations<RegisterModel>.DMLOperation(new RegisterModel
                {
                    OpMode = 1,
                    UserName = username,
                    Email = email,
                    PassCode = password,
                    PhoneNumber = phone
                }, Constant.usp_Register);

                return Json(new { success = true, redirectUrl = Url.Action("Login", "Account") });
            }
        }
    }
}
