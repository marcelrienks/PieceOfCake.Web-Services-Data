using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Services;

namespace Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutoMapperConfig.ConfigureMappings();
        }
    }
}