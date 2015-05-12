using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Language.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.RegisterComponents();
        }

        protected void Application_BeginRequest()
        {
            if(HttpContext.Current.Request != null && 
                HttpContext.Current.Request.Cookies["language"] != null)
            {
                CultureInfo culture = null;

                if(HttpContext.Current.Request.Cookies["language"].Value == "1")
                {
                    culture = new CultureInfo("nl-BE");
                }

                else
                {
                    culture = new CultureInfo("en-US");
                }

                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;

                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;
            }

            else
            {
                CultureInfo culture = CultureInfo.CurrentCulture;
            }
        }
    }
}
