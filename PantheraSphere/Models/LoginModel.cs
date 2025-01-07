using System.ComponentModel.DataAnnotations;

namespace PantheraSphere.Models
{
    public class LoginModel
    {
        public int OpMode { get; set; }
        public string UserName { get; set; }
        public string PassCode { get; set; }
        public int RoleID { get; set; }
    }
}
