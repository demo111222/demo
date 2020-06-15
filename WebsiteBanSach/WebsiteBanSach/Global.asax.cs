using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebsiteBanSach.Models;

namespace WebsiteBanSach
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Database.SetInitializer<BookStoreDB>(new DropCreateDatabaseIfModelChanges<BookStoreDB>());
        }
        protected void Session_Start()
        {
            Session["Taikhoan"] = null;
            Session["ID"] = null;
            Session["UserName"] = null;
            Session["UserGroup"] = null;
            Session["Name"] = null;
            Session["Avatar"] = null;
            Session["Phone"] = null;
            Session["Email"] = null;
            
        }
    }
}
