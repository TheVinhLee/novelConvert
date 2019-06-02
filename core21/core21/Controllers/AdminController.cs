using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using core21.Models;
using MySql.Data;
using System;

namespace core21.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index(string id="0")
        {
            UserDBModel db = new UserDBModel();
            if(Request.Cookies["userID"] != null)
            {
                id = Request.Cookies["userID"];
            }
            
            UserModel user = db.GetUserById(Int32.Parse(id));
            List<UserModel> nv = db.GetAllUser(user.fUsername,user.fPassword);
            if(!db.AdminChecking(Int32.Parse(id))) return RedirectToAction("../Home/Index");
        
            return View(nv);
        }
        
        public IActionResult Delete(int id, int value)
        {
            UserDBModel db = new UserDBModel();
            bool removeUser = db.RemoveUserById(id.ToString());
            if(removeUser) return Redirect(@"../Index/?id=" + value);

            return View("Cannot remove this user");

        }

        public IActionResult Details(int id)
        {
            return Redirect("~/User/Index/?id="+id);
        }
    }
}
