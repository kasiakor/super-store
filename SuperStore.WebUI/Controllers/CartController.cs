using SuperStore.Domain.Abstract;
using SuperStore.Domain.Entities;
using SuperStore.WebUI.Models;
using System.Linq;
using System.Web.Mvc;

namespace SuperStore.WebUI.Controllers
{

    public class CartController : Controller
    {
        private IProductRepository repository;
        private IOrderProcessor orderProcessor;

        public CartController(IProductRepository repo, IOrderProcessor proc)
        {
            repository = repo;
            orderProcessor = proc;

        }

        //public ViewResult Index(string returnUrl)
        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                //Cart = GetCart(),
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }
        //parameters match input elements in the html form
        //for model binding add Cart cart
        //mvc receives request for addto cart method it looks at itys params and tries to find binders to create instances
        //custom binder is asked to create cart object by working with the session state feature 
        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                cart.AddItem(product, 1);
                //GetCart().AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                cart.RemoveLine(product);
                //GetCart().RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            //it returns default view and passes shipping details object as view model to the view
            return View(new ShippingDetails());
        }

        //IOrderProcessor - void ProcessOrder(Cart cart, ShippingDetails shippingDetails);

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("ErrorMessage", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }









        ////removed for model binding!!!
        ////we use session state feature - GetCart() - to store and retrieve cart objects. It uses cookies or Url rewriting to associate multiple requests to form a single browsing session/cart persistent between requests
        //private Cart GetCart()
        //{
        //    // Session["Cart"] = cart, adds object to session state, set the value for a key on the Session object
        //    //Cart cart = (Cart)Session["Cart"], read the value, retrieve the object
        //    //session objects are stored in the memory of theASP.NET server by default
        //    Cart cart = (Cart)Session["Cart"];
        //    if (cart == null)
        //    {
        //        cart = new Cart();
        //        Session["Cart"] = cart;
        //    }
        //    return cart;
        //}
    }
}
