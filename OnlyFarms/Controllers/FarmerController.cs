using Microsoft.AspNetCore.Mvc;

namespace OnlyFarms.Controllers
{
    public class FarmerController : Controller
    {
        // 
        // GET: /Farmer/ 

        public IActionResult Index()
        {
            return View();
        }
    }
}
