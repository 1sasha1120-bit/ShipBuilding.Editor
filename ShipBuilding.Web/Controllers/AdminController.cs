using Microsoft.AspNetCore.Mvc;

namespace ShipBuilding.Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
