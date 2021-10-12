using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreApp.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        public ActionResult Home(int id)
        {
            StoreApp.Models.Store_DBEntities db = new Models.Store_DBEntities();

            var r = (from Itms in db.Items where Itms.Cat_ID == id select Itms).ToList();
            //Counter + 1
            var v = db.Vistors.Find(1);
            v.number = v.number + 1;
            db.SaveChanges();

            // Select Counter
            var v1 = db.Vistors.Find(1);
            ViewBag.vists = v1.number;

            //Sum of cat.
            var ca = (from c in db.Categories select c).ToList();
            ViewBag.cat_count = ca.Count();

            //Sum of Itms.
            var itm = (from c in db.Items select c).ToList();
            ViewBag.itm_count = itm.Count();

            return View("Home", r);
        }

        [HttpPost]
        public ActionResult Home(int Categories, string Search,int id)
        {
            StoreApp.Models.Store_DBEntities db = new Models.Store_DBEntities();

            var r = (from i in db.Items where i.Cat_ID == Categories && i.Item_name.Contains(Search) select i).ToList();
            if (r.Count<=0)
            {
                 r = (from i in db.Items where i.Cat_ID == id select i).ToList();
                return View(r);
            }
            return View(r);

            //int ID = int.Parse(Categories);
            //var r = (from itms in db.Items where itms.Item_name == Search select itms).ToList();
            //if (ID.ToString() == "Search")
            //{
            //    r = (from itms in db.Items where itms.Item_name == Search select itms).ToList();
            //}
            //return View("Home", r);
        }

        [HttpGet]
        public ActionResult Sign_in()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Sign_in(string UserName, string Password)
        {
            StoreApp.Models.Store_DBEntities db = new Models.Store_DBEntities();

            var result = (from i in db.Admins where i.UserName == UserName && i.Pwd == Password select i).ToList();
            if (result.Count >= 1)
            {
                return RedirectToAction("Home");
            }
            else
            {
                ViewBag.msg = "*Sorry you haven't permission to login !!";
                //ViewBag.imgerror_msg = "";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string UserName,string Password)
        {
            StoreApp.Models.Store_DBEntities db = new Models.Store_DBEntities();

            var result = (from i in db.Admins where i.UserName == UserName && i.Pwd == Password select i).ToList();
            if(result.Count >= 1)
            {
                return RedirectToAction("Admin");
            }
            else
            {
                ViewBag.msg = "*Sorry you haven't permission to login !!";
                //ViewBag.imgerror_msg = "";
                return View();
            }
        }

        //[HttpPost]
        //public ActionResult AddNewItem(int Categories, string Item_Name, string Price)
        //{
        //    // Save file to disk
        //    var File1 = System.Web.HttpContext.Current.Request.Files[0];
        //    if (File1.ContentLength > 0)
        //    {
        //        string filePath =
        //        Path.Combine(HttpContext.Server.MapPath("~/Images/Items"), Path.GetFileName(File1.FileName));
        //        File1.SaveAs(filePath);
        //    }

        //    // Save data into database
        //    StoreApp.Models.Store_DBEntities db = new StoreApp.Models.Store_DBEntities();
        //    db.Items.Add(new Models.Item
        //    {
        //        Cat_ID = Categories,
        //        Item_name = Item_Name,
        //        Item_price = decimal.Parse(Price),
        //        Item_img = File1.FileName
        //    });

        //    db.SaveChanges();
        //    return View(); ;
        //}


        [HttpGet]
        public ActionResult Admin()
        {
            StoreApp.Models.Store_DBEntities db = new Models.Store_DBEntities();
            var c = (from i in db.Categories select i).ToList();
            //var itm = (from i in db.Items select i).ToList();
            
            return View(c);

        }

        [HttpPost]
        public ActionResult AddNewCategory(string cat_txt)
        {
            StoreApp.Models.Store_DBEntities db = new Models.Store_DBEntities();
            db.Categories.Add(new Models.Category { Cat_Name = cat_txt });
            db.SaveChanges();

            return RedirectToAction("Admin");
        }

        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            StoreApp.Models.Store_DBEntities db = new Models.Store_DBEntities();
            var r = new StoreApp.Models.Category();
            r = db.Categories.Find(id);
            return View(r);
        }

        [HttpPost]
        public ActionResult EditCategory(string cat_txt, int id)
        {
            StoreApp.Models.Store_DBEntities db = new Models.Store_DBEntities();
            var r = new StoreApp.Models.Category();
            r = db.Categories.Find(id);
            r.Cat_Name = cat_txt;
            db.SaveChanges();

            return RedirectToAction("Admin");
        }

        public ActionResult DeleteCategory(int id)
        {
            var db = new StoreApp.Models.Store_DBEntities();
            //Find Cat id
            var r = db.Categories.Find(id);

            db.Categories.Remove(r);
            db.SaveChanges();

            return RedirectToAction("Admin");
        }


        [HttpGet]
        public ActionResult AddNewItem()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewItem(int Categories, string Itemname,
                                       string Price, string ItemQuantity, string ItemDetails,string FilePath1)
        {

            //Save file to disk
            //var File1 = System.Web.HttpContext.Current.Request.Files[0];
            //if (File1.ContentLength > 0)
            //{
            //    FilePath1=
            //    Path.GetExtension("~/Images/Items/") + Path.GetFileName(File1.FileName.ToLower());
            //    File1.SaveAs(FilePath1);
            //}

            StoreApp.Models.Store_DBEntities db = new Models.Store_DBEntities();
            db.Items.Add(new Models.Item {

                Cat_ID = Categories,
                Item_name = Itemname,
                Item_img = FilePath1,
                Item_price = decimal.Parse(Price),
                Item_quantity = decimal.Parse(ItemQuantity),
                Item_details = ItemDetails
            });

            db.SaveChanges();
            return RedirectToAction("Admin");
        }


        [HttpGet]
        public ActionResult EditItem(int id)
        {
            StoreApp.Models.Store_DBEntities db = new Models.Store_DBEntities();
            var r = new StoreApp.Models.Item();
            r = db.Items.Find(id);
            return View(r);
        }

        [HttpPost]
        public ActionResult EditItem(int id, int Categories, string Itemname,string Itemimg,
                                       string ItemPrice, string ItemQuantity, string ItemDetails)
        {
            StoreApp.Models.Store_DBEntities db = new Models.Store_DBEntities();
            var r = new StoreApp.Models.Item();
            r = db.Items.Find(id);
            r.Cat_ID = Categories;
            r.Item_name = Itemname;
            r.Item_img = Itemimg;
            r.Item_price = decimal.Parse(ItemPrice);
            r.Item_quantity = decimal.Parse(ItemQuantity);
            r.Item_details = ItemDetails;

            db.SaveChanges();

            return RedirectToAction("Admin");
        }

        public ActionResult DeleteItem(int id)
        {
            var db = new StoreApp.Models.Store_DBEntities();
            //Find item id
            var r = db.Items.Find(id);

            db.Items.Remove(r);
            db.SaveChanges();

            return RedirectToAction("Admin");
        }


        public ActionResult Item_details(int id)
        {
            ViewBag.id = id;
            return View();
        }

        [HttpGet]
        public ActionResult Conect()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Conect(string Sname,string Stel,string Smail,string Msg_txt)
        {
            StoreApp.Models.Store_DBEntities db = new Models.Store_DBEntities();
            db.Messages.Add(new Models.Message {
                Sname = Sname,
                Stel  = Stel,
                Smail = Smail,
                Msg = Msg_txt 
            });
            db.SaveChanges();
            
            return RedirectToAction("Home");
        }
    }
}