using ClassLibrary1;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        readonly ICarService carService;

        public HomeController(ICarService carService)
        {
            this.carService = carService;
        }

        public ActionResult Index()
        {
            return Content(this.carService.Run());
        }

    }
}