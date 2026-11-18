using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShipBuilding.Entities;
using ShipBuilding.Repositories.Extensions;
using ShipBuilding.Repositories.Interfaces;
using ShipBuilding.Web.Models.Extensions;
using ShipBuilding.Web.Models.ShipProjectModels;
using static ShipBuilding.Web.Models.ShipProjectModels.ShipProjectSelectionModel;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Web.Controllers
{
    public class ShipProjectsCrudController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IShipProjectsRepository _repository;
        private readonly IEnumerable<ShipProject> _objects;

        public ShipProjectsCrudController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.ShipProjectsRepository;
            _objects = _repository.GetAll();

        }

        //private IEnumerable<ShipProject> GetAll() => _objects;
        private SelectList CreateShipyardSelectList(string? selectedValue = "")
        {
            IEnumerable<string?> names = _unitOfWork.ShipyardsRepository.GetAll()
                .Select(e => e.Name).OrderBy(e => e);
            return new SelectList(names, selectedValue);
        }

        // ---------------------------------------------------------------------------

        public IActionResult Index(ShipProjectsFilter filter,
           ShipProjectsSortingInfo sortingInfo, string? sortFieldName)
        {
            filter      ??= new();
            sortingInfo ??= new();
            filter.ShipyardNamesList = CreateShipyardSelectList(filter.ShipyardName);
            var model = new ShipProjectSelectionModel(filter, sortingInfo, _objects, sortFieldName);
            return View(model);
        }
        // ---------------------------------------------------------------------------

        [HttpGet]
        public ActionResult Details(int id)
        {   
            ShipProject shipProject = _repository.GetById(id);
            if (shipProject == null) return NotFound();

            ShipProjectEditingModel model = (ShipProjectEditingModel)shipProject;
            return View(model);
        }

        // ---------------------------------------------------------------------------

        [HttpGet]
        public ActionResult Create()
        {
            var model = new ShipProjectEditingModel
            {
                ShipyardSelectList = CreateShipyardSelectList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ShipProjectEditingModel model)
        {
            ShipProject? obj;
            if (ModelState.IsValid)
            {
                obj = model.ToShipProject(
                    _unitOfWork.ShipyardsRepository.GetAll());
                if(!obj.CheckCreation(_unitOfWork, out string? message))
                {
                    ModelState.AddModelError("", message!);
                }
                else
                {
                    try
                    {
                        _repository.Add(obj);
                        _unitOfWork.Save();

                        TempData["message"] = $"Дані про {ProjectCaption.ToLower()} \"{obj.Key}\" додано";
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("",
                            $"Помилка створення запису про {ProjectCaption.ToLower()} : " + ex.Message);
                    }
                }
            }
            model.ShipyardSelectList = CreateShipyardSelectList(model.ShipyardName);
            return View(model);
        }

        // ---------------------------------------------------------------------------

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = _repository.GetById(id);

            if (obj == null) return NotFound();

            var model = (ShipProjectEditingModel)obj;
            model.ShipyardSelectList = CreateShipyardSelectList(model.ShipyardName);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ShipProjectEditingModel model)
        {
            ShipProject? obj;
            if (ModelState.IsValid)
            {
                obj = model.ToShipProject(
                    _unitOfWork.ShipyardsRepository.GetAll());
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

                        TempData["message"] = $"Дані про {ProjectCaption.ToLower()} \"{obj.Key}\" змінено";
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("",
                            $"Помилка редагування запису про {ProjectCaption.ToLower()}: " + ex.Message);
                    }
                }
            }
            model.ShipyardSelectList = CreateShipyardSelectList(model.ShipyardName);
            return View(model);
        }

        // ---------------------------------------------------------------------------

        [HttpGet]
        public ActionResult Delete(int id)
        {
            ShipProject? obj = _objects.FirstOrDefault(e => e.Id == id);
            if (obj == null) return NotFound();

            var model = (ShipProjectBrowsingModel)obj;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ShipProjectBrowsingModel model)
        {
            ShipProject? obj = _objects.FirstOrDefault(e => e.Id == model.Id);
            if (obj == null)
            {
                TempData["message"] = $"Дані про {ProjectCaption.ToLower()} \"{model.Id}\" відсутні у сховищі";
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
                TempData["message"] = $"Дані про {ProjectCaption.ToLower()} \"{obj.Key}\" видалено";
            }
            return RedirectToAction("Index");
        }
    }
}
