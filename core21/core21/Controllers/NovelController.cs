using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core21.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Web;

namespace core21.Controllers
{
    public class NovelController : Controller
    {

        [HttpGet]
        public IActionResult Edit(int id)
        {
            DBModel db = new DBModel();
            NovelModel novel = db.SelectOneNovel(id.ToString());
            
            return View(novel);
        }

        [HttpPost]
        public IActionResult Edit(NovelModel novel)
        {
            DBModel db = new DBModel();
            bool editCheck = db.EditNovel(Int32.Parse(novel.Id), novel);

            if (editCheck)
            {
                return Redirect("/Novel/Detail/?id=" + novel.Id);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Upload()
        {

            if(Request.Cookies["userID"] != null)
            {
                NovelModel nv = new NovelModel();
                nv.Owner = Request.Cookies["userID"];
                return View(nv);
            }
            else
            {
                return Redirect("/Home/Index/");
            }
        }

        [HttpPost]
        public IActionResult Upload(NovelModel novel)
        {
            if(novel.Name != null && novel.Author!= null && novel.Link != null)
            {
                novel.upload_date = DateTime.Now;
                novel.Owner = Request.Cookies["userID"].ToString();

                string image_sourceFile = @"C:\Users\lythe\Desktop\" + novel.Image_link;
                string image_destinationFile = @"~/database/Image/" + novel.Image_link;

                string sourceFile = @"C:\Users\lythe\Desktop\" + novel.Link;
                string destinationFile = "~/database/novel_book/" + novel.Link;

                System.IO.File.Copy(image_sourceFile, image_destinationFile, true);
                System.IO.File.Copy(sourceFile, destinationFile, true);

                DBModel db = new DBModel();

                int chap = 1;
                //get maximum chapter
                using (StreamReader sr = new StreamReader("~/database/novel_book/" + novel.Link))
                {
                    string line = sr.ReadLine(); //first is a chapter
                    while (line != null)
                    {
                        if (line.ToLower() == ("chapter " + chap))
                        {
                            chap += 1;
                        }

                        line = sr.ReadLine();
                    }
                }

                novel.Chap_number = chap-1;

                db.AddNewNovel(novel);
                
                return Redirect("/Home/Index/");
            }
            else
            {
                return Redirect("/Novel/Upload/");
            }

            
        }

        public IActionResult Detail(int id)
        {
            DBModel db = new DBModel();
            NovelModel novel = db.SelectOneNovel(id.ToString());

            return View(novel);
        }
    }
}
