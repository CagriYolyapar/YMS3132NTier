
using Projec.COMMON.MyTools;
using Project.BLL.RepositoryPattern.ConcRep;
using Project.MODEL.Entities;
using Project.MODEL.Enums;
using Project.MVCUI.Authentication;
using Project.MVCUI.AuthenticationClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminAuthentication = Project.MVCUI.AuthenticationClasses.AdminAuthentication;

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
        public ActionResult Register([Bind(Prefix = "item1")]AppUser item, [Bind(Prefix = "item2")]AppUserDetail item2)
        {
            apRep.CheckCredentials(item.UserName, item.Email, out bool varmi);
            if (varmi == false)
            {
                apRep.Add(item);
                item2.AppUser = item;
                apdRep.Add(item2);

                MailSender.Send(item.Email, body: $"{"http://localhost:60442/Home/RegisterOnay/"}{item.ActivationCode}", subject: "Doğrulama Kodu");

                ViewBag.mailOnay = "Aktivasyon Linki Gonderildi";

                return View();

            }

            ViewBag.ZatenVar = "Hesap Zatem Var";

            return View();
        }
        [HttpGet]
        public ActionResult RegisterOnay(Guid id)
        {
            AppUser kullanicionay = apRep.Where(x => x.ActivationCode == id).FirstOrDefault();

            if (kullanicionay != null)
            {
                kullanicionay.IsActive = true;
                apRep.Update(kullanicionay);
                TempData.Add("HesapAktif", $"{kullanicionay.UserName} Hesabınız Aktif Edildi");
                return RedirectToAction("Register");


            }
            ViewBag.mailonay = "Dogrulama Kodu Eksik Yada Hatalı";
            return RedirectToAction("Login");
        }
        

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        
        public ActionResult Login(AppUser item)
        {
            if (apRep.Any(x=>x.UserName==item.UserName && x.Password==item.Password && x.Role == UserRole.Member&&x.IsActive==true)==true)
            {
                Session.Add("member",apRep.Where(x=>x.UserName == item.UserName && x.Password == item.Password).FirstOrDefault());
                
            }
            else if (apRep.Any(x => x.UserName == item.UserName && x.Password == item.Password && x.Role == UserRole.Admin) == true)
            {
                Session.Add("admin", apRep.Where(x => x.UserName == item.UserName && x.Password == item.Password).FirstOrDefault());
            }
            else
            {
                // buraya gırıs yapmadı yazmak lazım
            }
                    

            return View();
        }

    }
}