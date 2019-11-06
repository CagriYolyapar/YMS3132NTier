using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Authentication
{
    public class AdminAuthentication:AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["admin"]!=null)
            {
                return true;
            }
            else
            {
                httpContext.Response.Redirect("/Home/Login");
                return false;
            }
        }
    }
}