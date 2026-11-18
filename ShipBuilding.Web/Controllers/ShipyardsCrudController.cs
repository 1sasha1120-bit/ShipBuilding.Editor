using Microsoft.AspNetCore.Mvc;
using ShipBuilding.Entities;
using ShipBuilding.Repositories.Extensions;
using ShipBuilding.Repositories.Interfaces;
using ShipBuilding.Web.Models.Extensions;
using ShipBuilding.Web.Models.ShipyardModels;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Web.Controllers
{
    public class ShipyardsCrudController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IShipyardsRepository _repository;
        private readonly IEnumerable<Shipyard> _objects;

        public ShipyardsCrudController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.ShipyardsRepository;
            _objects = _repository.GetAll();
        }

        // ---------------------------------------------------------------------------

        public IActionResult Index(ShipyardsFilter filter,
            ShipyardsSortingInfo sortingInfo, string? sortFieldName)
        {
            filter      ??= new();
            sortingInfo ??= new();
            var model = new ShipyardSelectionModel(filter, sortingInfo, _objects, sortFieldName);
            return View(model);
        }

        // ---------------------------------------------------------------------------

        [HttpGet]
        public ActionResult Details(int id)
        {
            var obj = _repository.GetById(id);
            if (obj == null) return NotFound();

            return View((ShipyardEditingModel)obj);
        }

        // ---------------------------------------------------------------------------

        [HttpGet]
        public ActionResult Create() => View(new ShipyardEditingModel());

        [HttpPost]
        public ActionResult Create(ShipyardEditingModel model)
        {
            if (ModelState.IsValid)
            {
                var obj = model.ToShipyard();
                if (!obj.CheckCreation(_unitOfWork, out string? message))
                {
                    ModelState.AddModelError("", message!);
                }
                else
                {
                    try
                    {
                        _repository.Add(obj);
                        _unitOfWork.Save();
                        TempData["message"] = $"Дані про {ShipyardCaption.ToLower()} \"{obj.Key}\" додано";
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("",
                            $"Помилка створення запису про {ShipyardCaption.ToLower()}: " + ex.Message);
                    }
                }
            }
            return View(model);
        }

        // ---------------------------------------------------------------------------

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = _repository.GetById(id);
            if (obj == null) return NotFound();
            return View((ShipyardEditingModel)obj);
        }

        [HttpPost]
        public ActionResult Edit(ShipyardEditingModel model)
        {
            if (ModelState.IsValid)
            {
                var obj = model.ToShipyard();
                if (!obj.CheckUpdating(_unitOfWork, out string? message))
                {
                    ModelState.AddModelError("", message!);
                }
                else
                {
                    try
                    {
                        _repository.Update(obj);
                        _unitOfWork.Save();
                        TempData["message"] = $"Дані про {ShipyardCaption.ToLower()} \"{obj.Key}\" змінено";
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("",
                            $"Помилка редагування запису про {ShipyardCaption.ToLower()}: " + ex.Message);
                    }
                }
            }
            return View(model);
        }

        // ---------------------------------------------------------------------------

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var obj = _objects.FirstOrDefault(e => e.Id == id);
            if (obj == null) return NotFound();
            return View(obj.ToDeletingModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ShipyardDeletingModel model)
        {
            var obj = _objects.FirstOrDefault(e => e.Id == model.Id);
            if (obj == null)
            {
                TempData["message"] = $"Дані про {ShipyardCaption.ToLower()} \"{model.Id}\" відсутні у сховищі";
            }
            else
            {
                if (!obj.CheckDeleting(_unitOfWork, out string? message))
                {
                    TempData["error"] = message;
                    return RedirectToAction("Delete", new { id = model.Id });
                }
                _repository.Delete(model.Id);
                _unitOfWork.Save();
                TempData["message"] = $"Дані про {ShipyardCaption.ToLower()} \"{obj.Key}\" видалено";
            }
            return RedirectToAction("Index");
        }
    }
}
