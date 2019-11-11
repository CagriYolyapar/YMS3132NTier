
using Projec.COMMON.MyTools;
using Project.BLL.RepositoryPattern.ConcRep;
using Project.MODEL.Entities;
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
        [HttpPost]
        public ActionResult Register([Bind(Prefix ="item1")]AppUser item)
        {
            apRep.Add(item);
            MailSender.Send(item.Email, body:$"http://localhost:60442/Home/Register2?id={item.ActivationCode}", subject: "Doğrulama Kodu");

          
            return View();
        }
        [HttpGet]
        public ActionResult Register2(string id)
        {
            return RedirectToAction("Register");
        }
    }
}