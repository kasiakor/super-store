using SuperStore.Domain.Abstract;
using System.Linq;
using System.Web.Mvc;
using SuperStore.Domain.Entities;
using System.Web;

namespace SuperStore.WebUI.Controllers
{
    //filter
    [Authorize]
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
        //add HttpPostedFileBase class that provides access to uploaded file by a client
        //HttpPostedFileBase properties: ContentType, ContentLength, InputStream
        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image = null)
        {
            if(ModelState.IsValid)
            {
                if(image != null)
                {
                    //copy data from the param valuie to the product object
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    //reads a sequence of bytes from the current stream: byte[] buffer, int offset, int count
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);

                }
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

        public ViewResult Create()
        {
            //new Product object is a view model
            return View("Edit", new Product());
        }
        [HttpPost]
        public ActionResult Delete(int productID)
        {
            Product deletedProduct = repository.DeleteProduct(productID);
            if(deletedProduct != null)
            {
                TempData["message"] = string.Format("{0} has been deleted", deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }
       
    }
}
