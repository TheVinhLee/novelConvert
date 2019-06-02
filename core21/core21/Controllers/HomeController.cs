using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using core21.Models;

namespace core21.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(int id = 0)
        {
            DBModel db = new DBModel();

            List<NovelModel> nv = db.TenNovel();

            return View(nv);
        }

        public IActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
