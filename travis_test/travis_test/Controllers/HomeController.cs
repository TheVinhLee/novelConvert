using System.Collections.Generic;
using System.Web.Mvc;

namespace travis_test.Controllers
{
    public class HomeController:Controller
    {
        public ActionResult Index(int i=0)
        {
            return View();
        }
    }
}