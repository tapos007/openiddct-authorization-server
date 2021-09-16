using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;

namespace Client1.Controllers
{
    [Route("api")]
    public class ResourceController : Controller
    {
        
        

        [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        [HttpGet("message")]
        public async Task<IActionResult> GetMessage()
        {

            var claims = User.Claims.ToList();
            return Content($"{string.Join(",",claims)} has been successfully authenticated.");
        }
    }
}