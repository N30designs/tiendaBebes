using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using universidadContoso.DAL;
using System.Data.Entity.Infrastructure.Interception;

namespace universidadContoso
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Añadido para log de sistema.
            DbInterception.Add(new EscuelaInterceptorTransientErrors());
            DbInterception.Add(new EscuelaInterceptorLogging());
        }
    }
}
