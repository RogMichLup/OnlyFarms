using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlyFarms.Controllers
{
    public class WorkerViewController : Controller
    {
        // 
        // GET: /WorkerView/ 
        public async Task<IActionResult> Index()
        {
            var claims = new List<Claim>()
                  {
                        new Claim(ClaimTypes.Name, "workerTest"),
                        new Claim(ClaimTypes.Role, "worker")
                    };
            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");
            await HttpContext.SignInAsync("CookieAuthentication", new ClaimsPrincipal(claimsIdentity));
            return View();
        }
    }
}
