using System.Linq;
using System.Web.Mvc;
using SportsStore.Domain.Entities;
using SuperStore.Domain.Abstract;
using SuperStore.Domain.Entities;

namespace SportsStore.WebUI.Controllers
{

    public class CartController : Controller
    {
        private IProductRepository repository;

        public CartController(IProductRepository repo)
        {
            repository = repo;
        }
        //parameters match input elements in the html form
        public RedirectToRouteResult AddToCart(int productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                GetCart().AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                GetCart().RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        //we use session state feature - GetCart() - to store and retrieve cart objects. It uses cooking or Url rewriting to associate multiple requests to form a single browsing session/cart persistend between requests
        private Cart GetCart()
        {
            // Session["Cart"] = cart, adds object to session state, set the value for a key on the Session object
            //Cart cart = (Cart)Session["Cart"], read the value, retrieve the object
            //session objects are stored in the memory of theASP.NET server by default
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
    }
}
