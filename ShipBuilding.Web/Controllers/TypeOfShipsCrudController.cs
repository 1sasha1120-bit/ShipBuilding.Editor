using Microsoft.AspNetCore.Mvc;
using ShipBuilding.Entities;
using ShipBuilding.Repositories.Extensions;
using ShipBuilding.Repositories.Interfaces;
using ShipBuilding.Web.Infrastructure;
using ShipBuilding.Web.Models.Extensions;
using ShipBuilding.Web.Models.TypeOfShipModels;
using static ShipBuilding.Settings.UiSettings;
using static ShipBuilding.Web.Resources.WebMessages;

namespace ShipBuilding.Web.Controllers
{
    public class TypeOfShipsCrudController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITypeOfShipsRepository _repository;
        private readonly IEnumerable<TypeOfShip> _objects;

        public TypeOfShipsCrudController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.TypeOfShipsRepository;
            _objects    = _repository.GetAll();
        }

        // ---------------------------------------------------------------------------

        public IActionResult Index(TypeOfShipsFilter filter, bool? expand)
        {
            filter ??= new();
            var treeData = TreeBuilder.BuildTree(_objects);
            var flatData = TreeBuilder.BuildFlatList(_objects);
            var model    = new TypeOfShipSelectionModel(filter, treeData, flatData, expand);
            return View(model);
        }

        // ---------------------------------------------------------------------------

        [HttpGet]
        public ActionResult Details(int id)
        {
            var obj = _repository.GetById(id);
            if (obj == null) return NotFound();
            return View((TypeOfShipEditingModel)obj);
        }

        // ---------------------------------------------------------------------------

        [HttpGet]
        public ActionResult Create() =>
            View(new TypeOfShipEditingModel { ParentSelectList = _objects.CreateParentSelectList() });

        [HttpPost]
        public ActionResult Create(TypeOfShipEditingModel model)
        {
            if (ModelState.IsValid)
            {
                var obj = model.ToTypeOfShip(_objects);
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
                        TempData["message"] = CreatedMessage(TypeOfShipCaption, obj.Key);
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("",
                            CreatingErrorMessage(TypeOfShipCaption, ex.Message));
                    }
                }
            }
            model.ParentSelectList = _objects.CreateParentSelectList(model.ParentName);
            return View(model);
        }

        // ---------------------------------------------------------------------------

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = _repository.GetById(id);
            if (obj == null) return NotFound();
            var model = (TypeOfShipEditingModel)obj;
            model.ParentSelectList = _objects.CreateParentSelectList(model.ParentName, id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(TypeOfShipEditingModel model)
        {
            if (ModelState.IsValid)
            {
                var obj = model.ToTypeOfShip(_objects);
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
                        TempData["message"] = UpdatedMessage(TypeOfShipCaption, obj.Key);
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("",
                            UpdatingErrorMessage(TypeOfShipCaption, ex.Message));
                    }
                }
            }
            model.ParentSelectList = _objects.CreateParentSelectList(model.ParentName, excludeId: model.Id);
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
        public ActionResult Delete(TypeOfShipDeletingModel model)
        {
            var obj = _objects.FirstOrDefault(e => e.Id == model.Id);
            if (obj == null)
            {
                TempData["message"] = NotFoundMessage(TypeOfShipCaption, model.Id);
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
                TempData["message"] = DeletedMessage(TypeOfShipCaption, obj.Key);
            }
            return RedirectToAction("Index");
        }
    }
}
