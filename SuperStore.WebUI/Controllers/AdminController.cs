using SuperStore.Domain.Abstract;
using System.Web.Mvc;


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
    }
}
