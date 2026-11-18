using Microsoft.AspNetCore.Mvc;
using ShipBuilding.Repositories.Interfaces;
using ShipBuilding.Web.Models;
using ShipBuilding.Web.Models.Extensions;
using System.Diagnostics;

namespace ShipBuilding.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View(_unitOfWork.GetDashboardModel());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
