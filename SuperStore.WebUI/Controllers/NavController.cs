using SuperStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        // GET: Nav
       
            private IProductRepository repository;
        
            public NavController(IProductRepository repo)
            {
                repository = repo;
            }

        //the value for category will be provided automaticaly by the routing configuration
        //public PartialViewResult Menu(string category = null) add horizontal menu option
        public PartialViewResult Menu(string category = null, bool horizontalMenu = false)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = repository.Products.Select(x => x.Category).Distinct().OrderBy(x => x);

            string viewName = horizontalMenu ? "MenuHorizontal" : "Menu";
            return PartialView(viewName, categories);
        }
    }
}