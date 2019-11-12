using Project.BLL.DesignPatterns.RepositoryPattern.ConcRep;
using Project.COMMON.MyTools;
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
        ProductRepository pRep;
        CategoryRepository cRep;

        public AdminProductController()
        {
            cRep = new CategoryRepository();
            pRep = new ProductRepository();
        }
        // GET: Administrator/AdminProduct
        public ActionResult ProductList()
        {
            return View(pRep.GetAll());
        }

        public ActionResult AddProduct()
        {
            return View(Tuple.Create(new Product(),cRep.GetActives()));
        }

        [HttpPost]
        public ActionResult AddProduct([Bind(Prefix ="Item1")] Product item,HttpPostedFileBase resim)
        {
            item.ImagePath = ImageUploader.UploadImage("~/Pictures/", resim);
            pRep.Add(item);
            return RedirectToAction("ProductList");
        }

        public ActionResult UpdateProduct()
        {
            

            return View();
        }

        [HttpPost]
        public ActionResult UpdateProduct(Product item)
        {
            Product guncellenecek = pRep.Find(item.ID);
            guncellenecek.ProductName = item.ProductName;
            guncellenecek.UnitPrice = item.UnitPrice;
            guncellenecek.UnitsInStock = item.UnitsInStock;
            pRep.Update(item);
            return RedirectToAction("ProductList");
        }

        public ActionResult DeleteProduct(int id)
        {
            pRep.Delete(pRep.Find(id));
            return RedirectToAction("ProductList");
        }
    }
}