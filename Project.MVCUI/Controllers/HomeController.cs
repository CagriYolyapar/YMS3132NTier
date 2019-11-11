using Project.BLL.RepositoryPattern.ConcRep;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class HomeController : Controller
    {

        AppUserRepository apRep;
        AppUserDetailRepository apdRep;

        public HomeController()
        {
            apRep = new AppUserRepository();

            apdRep = new AppUserDetailRepository();
        }


        public ActionResult Register()
        {
            return View();
        }
    }
}