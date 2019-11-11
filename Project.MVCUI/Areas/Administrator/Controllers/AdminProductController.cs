using Project.BLL.DesignPatterns.RepositoryPattern.ConcRep;
using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Areas.Administrator.Controllers
{
    public class AdminProductController : Controller
    {
        ProductRepository prep;

        public AdminProductController()
        {
            prep = new ProductRepository();
        }
        // GET: Administrator/AdminProduct
        public ActionResult ProductList()
        {
            return View(prep.GetAll());
        }

        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product item)
        {
            prep.Add(item);
            return RedirectToAction("ListProduct");
        }

        public ActionResult UpdateProduct()
        {
            

            return View();
        }

        [HttpPost]
        public ActionResult UpdateProduct(Product item)
        {
            Product guncellenecek = prep.Find(item.ID);
            guncellenecek.ProductName = item.ProductName;
            guncellenecek.UnitPrice = item.UnitPrice;
            guncellenecek.UnitsInStock = item.UnitsInStock;
            prep.Update(item);
            return RedirectToAction("ListProduct");
        }

        public ActionResult DeleteProduct(int id)
        {
            prep.Delete(prep.Find(id));
            return RedirectToAction("ListProduct");
        }
    }
}