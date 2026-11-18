using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShipBuilding.Entities;
using ShipBuilding.Repositories.Extensions;
using ShipBuilding.Repositories.Interfaces;
using ShipBuilding.Web.Infrastructure;
using ShipBuilding.Web.Models.Extensions;
using ShipBuilding.Web.Models.TypeOfShipModels;
using static ShipBuilding.Settings.UiSettings;

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

        private SelectList CreateParentSelectList(string? selectedValue = "", int? excludeId = null)
        {
            IEnumerable<TypeOfShip> candidates = _objects;

            if (excludeId.HasValue)
            {
                var excluded = new HashSet<int>();
                var queue = new Queue<int>();
                queue.Enqueue(excludeId.Value);
                while (queue.Count > 0)
                {
                    int current = queue.Dequeue();
                    excluded.Add(current);
                    foreach (var child in _objects.Where(e => e.Parent?.Id == current))
                        queue.Enqueue(child.Id);
                }
                candidates = candidates.Where(e => !excluded.Contains(e.Id));
            }

            var names = candidates.Select(e => e.Name).OrderBy(e => e);
            return new SelectList(names, selectedValue);
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
            View(new TypeOfShipEditingModel { ParentSelectList = CreateParentSelectList() });

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
                        TempData["message"] = $"Дані про {TypeOfShipCaption.ToLower()} \"{obj.Key}\" додано";
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("",
                            $"Помилка створення запису про {TypeOfShipCaption.ToLower()}: " + ex.Message);
                    }
                }
            }
            model.ParentSelectList = CreateParentSelectList(model.ParentName);
            return View(model);
        }

        // ---------------------------------------------------------------------------

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = _repository.GetById(id);
            if (obj == null) return NotFound();
            var model = (TypeOfShipEditingModel)obj;
            model.ParentSelectList = CreateParentSelectList(model.ParentName, id);
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
                        TempData["message"] = $"Дані про {TypeOfShipCaption.ToLower()} \"{obj.Key}\" змінено";
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("",
                            $"Помилка редагування запису про {TypeOfShipCaption.ToLower()}: " + ex.Message);
                    }
                }
            }
            model.ParentSelectList = CreateParentSelectList(model.ParentName, excludeId: model.Id);
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
                TempData["message"] = $"Дані про {TypeOfShipCaption.ToLower()} \"{model.Id}\" відсутні у сховищі";
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
                TempData["message"] = $"Дані про {TypeOfShipCaption.ToLower()} \"{obj.Key}\" видалено";
            }
            return RedirectToAction("Index");
        }
    }
}
