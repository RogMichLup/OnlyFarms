using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlyFarms.Controllers
{
    public class FarmerController : Controller
    {
        // 
        // GET: /Farmer/ 

        public async Task<IActionResult> Index()
        {
            var claims = new List<Claim>()
                  {
                        new Claim(ClaimTypes.Name, "farmerTest"),
                        new Claim(ClaimTypes.Role, "admin")
                    };
            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");
            await HttpContext.SignInAsync("CookieAuthentication", new ClaimsPrincipal(claimsIdentity));
            return View();
        }
    }
}
