using Project.BLL.DesignPatterns.RepositoryPattern.ConcRep;
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
       

    }
}