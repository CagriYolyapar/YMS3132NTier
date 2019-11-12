using Project.BLL.DesignPatterns.RepositoryPattern.ConcRep;
using Project.BLL.RepositoryPattern.ConcRep;
using Project.MODEL.Entities;
using Project.MVCUI.Models.CustomTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class MemberController : Controller
    {
        ProductRepository pRep;

        OrderRepository oRep;

        OrderDetailRepository odRep;

        CategoryRepository cRep;

        public MemberController()
        {
            pRep = new ProductRepository();
            oRep = new OrderRepository();
            odRep = new OrderDetailRepository();
            cRep = new CategoryRepository();

        }


        public ActionResult ProductList()
        {
            return View(pRep.GetActives());
        }


        public ActionResult SepeteAt(int id)
        {
            //if (Session["member"] == null)
            //{
            //    TempData["uyeDegil"] = "Lütfen sepete ürün eklemeden önce üye olun";
            //    Product bekleyenUrun = pRep.Find(id);
            //    Session["bekleyenUrun"] = bekleyenUrun;
            //    return RedirectToAction("Register", "Home");
            //}


            Cart c = Session["scart"] == null ? new Cart() : Session["scart"] as Cart;

            Product eklenecekUrun = pRep.Find(id);


            CartItem ci = new CartItem();

            ci.ID = eklenecekUrun.ID;

            ci.Name = eklenecekUrun.ProductName;

            ci.Price = eklenecekUrun.UnitPrice;

            ci.ImagePath = eklenecekUrun.ImagePath;

            c.SepeteEkle(ci);

            Session["scart"] = c;

            return RedirectToAction("ProductList");




        }



        public ActionResult CartPage()
        {
            if (Session["scart"] != null)
            {
                Cart c = Session["scart"] as Cart;

                return View(c);
            }

            else if (Session["member"] == null)
            {
                TempData["UyeDegil"] = "Lutfen önce üye olun";
                return RedirectToAction("Register", "Home");
            }

            TempData["message"] = "Sepetinizde ürün bulunmamaktadır";
            return RedirectToAction("ProductList");
        }



        public ActionResult SiparisiOnayla()
        {
            // if (Session["member"] != null)

            //  AppUser mevcutKullanici = Session["member"] as AppUser;

            //}
            // TempData["message"] = "Üye olmadan sipariş veremezsiniz";
            // return RedirectToAction("ProductList");  
            return View(Tuple.Create(new Order(), new PaymentVM()));
        }


        [HttpPost]

        public ActionResult SiparisiOnayla([Bind(Prefix = "Item1")] Order item, [Bind(Prefix = "Item2")] PaymentVM item2)
        {
            bool result = false;
            //Burada artık bir client olarak API'a istek göndermemiz lazım (API consume)..Bunun icin Microsoft.Asp.Net.WebApi.Client package'ini Nuget'tan indirmelisiniz. 

            //Aynı zamanda System.Net.Http.DLL'ini Assembly'den (Referanslara giderek) engtegre etmelisiniz.
            using (HttpClient client = new HttpClient())
            {

                //http://localhost:57177/api/Payment/ReceivePayment

                client.BaseAddress = new Uri("http://localhost:57177/api/");
                Cart sepet = Session["scart"] as Cart;
                item2.PaymentPrice = sepet.TotalPrice.Value;



                var postTask = client.PostAsJsonAsync("Payment/ReceivePayment", item2);

                //Burada item2 nesnesini json olarak gönderiyoruz. Ve Base Adresimizin üzerine Payment/ReceivePayment'i ekliyoruz..

                HttpResponseMessage sonuc = postTask.Result;

                if (sonuc.IsSuccessStatusCode)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }


            }

            if (result)
            {
                //AppUser kullanici = Session["member"] as AppUser;
                item.AppUserID = 7; //Order'in kim tarafından sipariş edildigini belirlersiniz
                oRep.Add(item); //save edildiginde Order nesnesinin ID'si üretilir.

                Cart sepet = Session["scart"] as Cart;

                foreach (CartItem urun in sepet.Sepetim)
                {
                    OrderDetail od = new OrderDetail();
                    od.OrderID = item.ID;
                    od.ProductID = urun.ID;
                    odRep.Add(od);

                }
                TempData["odeme"] = "Siparişiniz bize ulasmıstır..Tesekkür ederiz";
                return RedirectToAction("ProductList");

            }

            else
            {
                TempData["odeme"] = "Odeme ile ilgili bir sıkıntı olustu. Lütfen banka ile iletişime geciniz";
                return RedirectToAction("ProductList");
            }


        }




    }
}