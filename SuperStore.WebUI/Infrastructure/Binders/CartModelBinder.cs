using SuperStore.Domain.Entities;
using System.Web.Mvc;


namespace SuperStore.WebUI.Infrastructure.Binders
{
    //CartModelBinder creates cart instances. Add binding in app_start, global
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "Cart";
        //ControllerContext provides access to all information from controller/ client request details
        //ModelBindingContext gives information about model object to build and tools to make it easier
        public object BindModel(ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {

            // get the Cart from the session 
            //HttpContext.Session lets set and get session data
            Cart cart = null;
            if (controllerContext.HttpContext.Session != null)
            {
                cart = (Cart)controllerContext.HttpContext.Session[sessionKey];
            }
            // create the Cart if there wasn't one in the session data
            if (cart == null)
            {
                cart = new Cart();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = cart;
                }
            }
            // return the cart
            return cart;
        }
    }
}
