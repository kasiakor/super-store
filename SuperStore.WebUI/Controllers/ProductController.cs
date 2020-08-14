using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SuperStore.Domain.Abstract;
using SuperStore.Domain.Entities;

namespace SportsStore.WebUI.Controllers
{

    public class ProductController : Controller
    {
        private IProductRepository repository;
        //constructor that declares a dependency on the IProductRepository interface, Ninject will inject the dependency for the product repository when it instantiates the controller class
        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }

        public ViewResult List()
        {
            return View(repository.Products);
        }
    }
}