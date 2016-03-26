using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApplicationJedi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            /* rajouté par Benoît */
            string coco = AppDomain.CurrentDomain.BaseDirectory.Split(new string[] { "WebApplicationJedi" }, StringSplitOptions.RemoveEmptyEntries)[0]+"Database";
            AppDomain.CurrentDomain.SetData("DataDirectory", coco);
        }
    }
}
