using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShipBuilding.Entities;
using ShipBuilding.Repositories.Extensions;
using ShipBuilding.Repositories.Interfaces;
using ShipBuilding.Web.Models.Extensions;
using ShipBuilding.Web.Models.ShipModels;
using ShipBuilding.Web.Models.Ships;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Web.Controllers
{
    public class ShipsCrudController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IShipsRepository _repository;
        private readonly IEnumerable<Ship> _objects;

        public ShipsCrudController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.ShipsRepository;
            _objects = _repository.GetAll();
        }

        private SelectList CreateShipyardSelectList(string? selectedValue = "")
        {
            IEnumerable<string?> names = _unitOfWork.ShipyardsRepository.GetAll()
                .Select(e => e.Name).OrderBy(e => e);
            return new SelectList(names, selectedValue);
        }
        private SelectList CreateTypeOfShipSelectList(string? selectedValue = "")
        {
            IEnumerable<string?> names = _unitOfWork.TypeOfShipsRepository.GetAll()
                .Select(e => e.Name).OrderBy(e => e);
            return new SelectList(names, selectedValue);
        }
        private SelectList CreateProjectSelectList(string? selectedValue = "")
        {
            IEnumerable<string?> names = _unitOfWork.ShipProjectsRepository.GetAll()
                .Select(e => e.Name).OrderBy(e => e);
            return new SelectList(names, selectedValue);
        }

        // ---------------------------------------------------------------------------

        public IActionResult Index(ShipsFilter filter,
            ShipsSortingInfo sortingInfo, string sortFieldName)
        {
            filter ??= new();
            sortingInfo ??= new();
            filter.ShipyardNamesList = CreateShipyardSelectList(filter.ShipyardName);
            filter.TypeOfShipNamesList = CreateTypeOfShipSelectList(filter.TypeOfShipName);
            ShipSelectionModel model = new(filter, sortingInfo, _objects, sortFieldName);
            return View(model); 
        }

        // ---------------------------------------------------------------------------

        [HttpGet]
        public IActionResult Details(int id)
        {
            Ship ship = _repository.GetById(id);
            if (ship == null)
            {
                return NotFound(); 
            }
            ShipEditingModel model = (ShipEditingModel)ship;
            return View(model);
        }

        // ---------------------------------------------------------------------------

        [HttpGet]
        public IActionResult Create()
        {
            var model = new ShipEditingModel
            {
                ShipyardSelectList = CreateShipyardSelectList(),
                TypeOfShipSelectList = CreateTypeOfShipSelectList(),
                ProjectSelectList = CreateProjectSelectList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(ShipEditingModel model)
        {
            if (ModelState.IsValid)
            {
                var obj = model.ToShip(
                            _unitOfWork.ShipyardsRepository.GetAll(),
                            _unitOfWork.TypeOfShipsRepository.GetAll(),
                            _unitOfWork.ShipProjectsRepository.GetAll());
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

                        TempData["message"] = $"Дані про {ShipCaption.ToLower()} \"{obj.Key}\" додано";
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("",
                            $"Помилка створення запису про {ShipCaption.ToLower()} : " + ex.Message);
                    }
                }
            }
            model.ShipyardSelectList = CreateShipyardSelectList(model.ShipyardName);
            model.TypeOfShipSelectList = CreateTypeOfShipSelectList(model.TypeOfShipName);
            model.ProjectSelectList = CreateProjectSelectList(model.ProjectName);
            return View(model);
        }

        // ---------------------------------------------------------------------------

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Ship? ship = _repository.GetById(id);
            if (ship == null)
            {
                return NotFound();
            }
            var model = (ShipEditingModel)ship;
            model.ShipyardSelectList = CreateShipyardSelectList(model.ShipyardName);
            model.TypeOfShipSelectList = CreateTypeOfShipSelectList(model.TypeOfShipName);
            model.ProjectSelectList = CreateProjectSelectList(model.ProjectName);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ShipEditingModel model)
        {
            Ship? obj;
            if (ModelState.IsValid)
            {
                obj = model.ToShip(
                            _unitOfWork.ShipyardsRepository.GetAll(),
                            _unitOfWork.TypeOfShipsRepository.GetAll(),
                            _unitOfWork.ShipProjectsRepository.GetAll());
                if (!obj.CheckUpdating(_unitOfWork, out string message))
                {
                    ModelState.AddModelError("", message);
                }
                else
                {
                    try
                    {
                        _repository.Update(obj);
                        _unitOfWork.Save();

                        TempData["message"] = $"Дані про {ShipCaption.ToLower()} \"{obj.Key}\" змінено";
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("",
                            $"Помилка редагування запису про {ShipCaption.ToLower()}: " + ex.Message);
                    }
                }
            }
            model.ShipyardSelectList = CreateShipyardSelectList(model.ShipyardName);
            model.TypeOfShipSelectList = CreateTypeOfShipSelectList(model.TypeOfShipName);
            model.ProjectSelectList = CreateProjectSelectList(model.ProjectName);
            return View(model);
        }

        // ---------------------------------------------------------------------------

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Ship? obj = _objects.FirstOrDefault(e => e.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            var model = obj.ToDeletingModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // для захисту від CSRF-атак
        public IActionResult Delete(ShipDeletingModel model)
        {
            var obj = _objects.FirstOrDefault(e => e.Id == model.Id);
            if (obj == null)
            {
                TempData["message"] = $"Дані про {ShipCaption} \"{model.Id}\" відсутні у сховищі";
            }
            else
            {
                _repository.Delete(model.Id);
                _unitOfWork.Save();
                TempData["message"] = $"Дані про {ShipCaption} \"{obj.Key}\" видалено";
            }
            return RedirectToAction("Index");
        }
    }
}