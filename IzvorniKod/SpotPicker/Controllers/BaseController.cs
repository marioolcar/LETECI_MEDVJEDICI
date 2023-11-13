using Microsoft.AspNetCore.Mvc;

namespace SpotPicker.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BaseController : ControllerBase
    {
       public int KorisnikID { get
            {
                if (!string.IsNullOrEmpty(HttpContext.Request.Headers["username"]))
                {
                    return Convert.ToInt32(HttpContext.Request.Headers["username"]);
                }
                else return 0;
            } 
        }

        public int AccessLevel { get
            {
                if (!string.IsNullOrEmpty(HttpContext.Request.Headers["accessLevel"]))
                {
                    return Convert.ToInt32(HttpContext.Request.Headers["accessLevel"]);
                }
                else return 0;
            } 
        }
    }
}
