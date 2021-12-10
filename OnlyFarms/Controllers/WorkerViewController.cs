<<<<<<< Updated upstream
﻿using Microsoft.AspNetCore.Mvc;
=======
﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlyFarms.Data;
using OnlyFarms.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
>>>>>>> Stashed changes

namespace OnlyFarms.Controllers
{
    public class WorkerViewController : Controller
    {
<<<<<<< Updated upstream
        // 
        // GET: /WorkerView/ 

        public IActionResult Index()
        {
            return View();
=======
        private readonly FarmContext _context;
        private List<Worker> workers;
        public WorkerViewController(FarmContext context) {
            _context = context;
            workers = _context.Workers.ToList();
            ViewBag.workers = workers;
        }


        public IActionResult Index()
        {
            //var claims = new List<Claim>()
            //      {
            //            new Claim(ClaimTypes.Name, "worker#"+ID.ToString()),
            //            new Claim(ClaimTypes.Role, "worker")
            //        };
            //var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");
            //await HttpContext.SignInAsync("CookieAuthentication", new ClaimsPrincipal(claimsIdentity));
            return View();
        }
        public IActionResult Login() {
            workers = _context.Workers.ToList();
            ViewBag.workers = workers;
            return View();
        }
        public async Task<IActionResult> LoginConfirmed(int ID) {
            await HttpContext.SignOutAsync();
            var claims = new List<Claim>()
                  {
                        new Claim(ClaimTypes.Name, "worker#"+ID.ToString()),
                        new Claim(ClaimTypes.Role, "worker")
                    };
            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");
            await HttpContext.SignInAsync("CookieAuthentication", new ClaimsPrincipal(claimsIdentity));
            return RedirectToAction(nameof(Index));
>>>>>>> Stashed changes
        }
    }
}
