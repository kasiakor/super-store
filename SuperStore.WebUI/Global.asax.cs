using SuperStore.Domain.Entities;
using SuperStore.WebUI.Infrastructure.Binders;
using System.Web.Mvc;
using System.Web.Routing;

namespace SuperStore.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //we tell Mvc Framework to use the CartModelBinder to create instances of Cart
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
        }
    }
}
