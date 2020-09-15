using SuperStore.Domain.Abstract;
using System.Linq;
using System.Web.Mvc;
using SuperStore.Domain.Entities;

namespace SuperStore.WebUI.Controllers
{

    public class AdminController : Controller
    {
        private IProductRepository repository;

        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Products);
        }

        public ViewResult Edit(int productId)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            return View(product);
        }

        //invoked when user click save button on edit product page
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if(ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been updated", product.Name);
                return RedirectToAction("Index");
            }

            else
            {
                //something went wrong
                return View(product);
            }
        }
    }
}
