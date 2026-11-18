using Microsoft.AspNetCore.Mvc;
using ShipBuilding.Repositories.Interfaces;
using ShipBuilding.Web.Models.Extensions;

namespace ShipBuilding.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View(_unitOfWork.GetAdminPanelModel());
        }
    }
}
