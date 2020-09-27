using System.Linq;
using System.Web.Mvc;
using SuperStore.Domain.Abstract;
using SuperStore.Domain.Entities;
using SuperStore.WebUI.Models;

namespace SuperStore.WebUI.Controllers
{

    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 4;

        //constructor that declares a dependency on the IProductRepository interface, Ninject will inject the dependency for the product repository when it instantiates the controller class
        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }

        public ViewResult List(string category, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = repository.Products.Where(p => category == null || p.Category == category).OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? repository.Products.Count() : repository.Products.Where(e => e.Category == category).Count()
                },
                    CurrentCategory = category
            };
            //sequence of product objects passed to the view
            return View(model);
        }

        public FileContentResult GetImage(int productId)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if(product != null)
            {
                //creates instance when we return file to the client browser
                return File(product.ImageData, product.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}
