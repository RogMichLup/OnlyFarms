using Microsoft.AspNetCore.Mvc;

namespace OnlyFarms.Controllers
{
    public class WorkerViewController : Controller
    {
        // 
        // GET: /WorkerView/ 

        public IActionResult Index()
        {
            return View();
        }
    }
}
