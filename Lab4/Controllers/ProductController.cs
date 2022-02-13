using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Lab4.Models.Database;
namespace Lab4.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            sp22BEntities3 db = new sp22BEntities3();
            var data = db.Products.ToList();
            return View(data);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Product());
        }
        [HttpPost]
        public ActionResult Create(Product p)
        {
            if (ModelState.IsValid)
            {
                sp22BEntities3 db = new sp22BEntities3();
                db.Products.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            sp22BEntities3 db = new sp22BEntities3();
            var data = (from p in db.Products
                        where p.pId == Id
                        select p).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(Product pp)
        {
            sp22BEntities3 db = new sp22BEntities3();
            var data = (from b in db.Products
                        where b.pId == pp.pId
                        select b).FirstOrDefault();
            db.Entry(data).CurrentValues.SetValues(pp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            sp22BEntities3 db = new sp22BEntities3();
            var data = (from p in db.Products
                        where p.pId == Id
                        select p).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public ActionResult Delete(Product ps)
        {
            sp22BEntities3 db = new sp22BEntities3();
            var data = (from b in db.Products
                        where b.pId == ps.pId
                        select b).FirstOrDefault();
            db.Products.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Cart(int id)
        {
            List<Product> stData = new List<Product>();
            sp22BEntities3 db = new sp22BEntities3();
            var data = (from b in db.Products
                        where b.pId == id
                        select b).FirstOrDefault();

            if (Session["cart"] == null)
            {
                stData.Add(data);
                string json = new JavaScriptSerializer().Serialize(stData);
                Session["cart"] = json;
            }
            else
            {
                string json = Session["cart"].ToString();
                 var d=new JavaScriptSerializer().Deserialize<List<Product>>(json);
                d.Add(data);
                string json1 = new JavaScriptSerializer().Serialize(d);
                Session["cart"] = json1;
            }
            return View("Index");
        }
        public ActionResult Viewcart()
        {
            var data = Session["cart"];
            return View(data);
        }

    }
}