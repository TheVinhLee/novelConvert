using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using core21.Models;
using MySql.Data;
using System;

namespace core21.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index(int id=0)
        {
            if(Request.Cookies["userID"] != null)
            {
                string userID = Request.Cookies["userID"];

                UserDBModel db = new UserDBModel();
                UserModel user = db.GetUserById(Int32.Parse(userID));
                //get the total novel uploaded

                return View(user);
            }
            else
            {
                return RedirectToAction("/Home/Index/");
            }

            
        }
        
        public IActionResult Upload(int id = 0)
        {
            if (Request.Cookies["userID"] != null)
            {
                return RedirectToAction("/Novel/Upload/");
            }
            else
            {
                return View();
            }
            
        }

        [HttpGet]
        public IActionResult Edit(int id=0)
        {
            UserDBModel db = new UserDBModel();
            if(Request.Cookies["userID"] != null)
            {
                id = Int32.Parse(Request.Cookies["userID"]);
            }
            else
            {
                return RedirectToAction("/Home/Index/");
            }
            UserModel user = db.GetUserById(id);

            return View(user);
        }
        
        [HttpPost]
        public IActionResult Edit(UserModel user)
        {
            UserDBModel db = new UserDBModel();
            bool userEdit = db.EditUser(Int32.Parse(user.fID), user);

            if (userEdit)
            {
                return Redirect("Index/?Id=" + user.fID);
            }
            return Redirect("Edit/?Id=" + user.fID);
        }

        [HttpGet]
        public IActionResult Register(int id = 0)
        {
            UserModel user = new UserModel();

            return View(user);
        }

        [HttpPost]
        public IActionResult Register(UserModel user)
        {
            UserDBModel db = new UserDBModel();
            UserModel luser = db.UserRegister(user.fUsername, user.fPassword);
            return RedirectToAction("../Home/Index");
        }

        public IActionResult Login()
        {
            return RedirectToAction("/Register/");
        }

        [HttpPost]
        public IActionResult Login(UserModel user)
        {
            if(user.fUsername != null && user.fPassword != null)
            {
                UserDBModel db = new UserDBModel();
                UserModel luser = db.UserLogin(user.fUsername, user.fPassword);

                if(luser.fUsername != null)
                {
                    Response.Cookies.Append("user", luser.fUsername);
                    Response.Cookies.Append("userID", luser.fID);

                    if (db.AdminChecking(luser.fUsername, luser.fPassword))
                    {
                        string id = luser.fID;
                        return Redirect(@"../Admin/Index/?id=" + id);
                    }
                    return Redirect("../Home/Index/?id=" + luser.fID);
                }
                else
                {
                    ModelState.AddModelError("","Wrong username or password");
                }
               
            }
            else
            {
                ModelState.AddModelError("", "should not empty username or password");
            }
            return RedirectToAction("../User/Login/");
        }

        public IActionResult LogOff()
        {
            if(Request.Cookies["user"] != null)
            {
                Response.Cookies.Delete("user");
                Response.Cookies.Delete("userID");
            }

            return Redirect("/Home/Index/");
        }

        public IActionResult MyNovel(int id = 0)
        {
            DBModel db = new DBModel();

            if (Request.Cookies["userID"] != null)
            {
                id = Int32.Parse(Request.Cookies["userID"]);
                //get all user novel
                List<NovelModel> listNovel = db.GetAllNovelByUserId(id);

                return View(listNovel);
            }
            else
            {
                return RedirectToAction("../Home/Index/");
            }

        }

        public IActionResult MyNovelDetail(int id = 0)
        {
            if(id != 0)
            {
                return Redirect("/Novel/Detail/?id="+id);
            }
            else{
                return RedirectToAction("../Home/Index/");
            }
        }

        public IActionResult Delete(int id =0)
        {
            DBModel db = new DBModel();

            if(Request.Cookies["userID"]!=null)
            {
                bool delete = db.RemoveNovelById(Request.Cookies["userID"]);

                if (delete)
                {
                    return RedirectToAction("/MyNovel/");
                }
                else
                {
                    return View();
                }

            }
            
            return View();
        }
    }
}
