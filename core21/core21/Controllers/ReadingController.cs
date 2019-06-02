using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core21.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace core21.Controllers
{
    public class ReadingController: Controller
    {
        public IActionResult Voting(int id, string chapter)
        {
            DBModel db = new DBModel();
            bool nv = db.NovelVoted(id);
            return Redirect("~/Reading/?id=" + id + "&chapter=" + chapter + "");
        }

        public IActionResult Index(string id, string chapter)
        {
            DBModel db = new DBModel();
            NovelModel nv = db.SelectOneNovel(id);

            //increasing view
            nv.Viewer += 1;
            bool viewChecking = db.EditNovel(Int32.Parse(id), nv);

            ViewBag.BookID = id;

            if(chapter == null)
            {
                chapter = "1";
            }
            

            ViewBag.Chapter = chapter;
            ViewBag.NextChapter = Int32.Parse(chapter) + 1;
            ViewBag.Previous = (Int32.Parse(chapter) > 1) ? Int32.Parse(chapter) - 1 : Int32.Parse(chapter);
            int chap = 1;
            //get maximum chapter
            using (StreamReader sr = new StreamReader("./database/novel_book/" + nv.Link))
            {
                string line = sr.ReadLine(); //first is a chapter
                while (line != null)
                {
                    if(line.ToLower() == ("chapter "+chap))
                    {
                        chap += 1;
                    }
                    
                    line = sr.ReadLine();
                }
            }
            if(ViewBag.NextChapter > chap - 1)
            {
                ViewBag.NextChapter = chap-1;
            }
            
            return View(nv);
        }
    }
}
