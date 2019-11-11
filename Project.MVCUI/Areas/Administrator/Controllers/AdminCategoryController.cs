using Project.BLL.DesignPatterns.RepositoryPattern.ConcRep;
using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Areas.Administrator.Controllers
{
    public class AdminCategoryController : Controller
    {

        CategoryRepository crep;

        public AdminCategoryController()
        {
            crep = new CategoryRepository();
        }



        public ActionResult CategoryList()
        {

            return View(crep.GetAll());
        }


        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category item)
        {
            crep.Add(item);
            return RedirectToAction("CategoryList");
        }

        public ActionResult UpdateCategory(int id)
        {
            return View(crep.Find(id));
        }
        [HttpPost]
        public ActionResult UpdateCategory(Category item)
        {
            crep.Update(item);
            return RedirectToAction("CategoryList");
        }

        public ActionResult DeleteCategory(int id)
        {
            crep.Delete(crep.Find(id));
            return RedirectToAction("CategoryList");
        }
    }
}