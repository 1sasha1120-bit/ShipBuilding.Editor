using Microsoft.AspNetCore.Mvc;
using ShipBuilding.Entities;
using ShipBuilding.Repositories.Extensions;
using ShipBuilding.Repositories.Interfaces;
using ShipBuilding.Web.Models.Extensions;
using ShipBuilding.Web.Models.ShipModels;
using ShipBuilding.Web.Models.Ships;
using static ShipBuilding.Settings.UiSettings;
using static ShipBuilding.Web.Resources.WebMessages;

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


        // ---------------------------------------------------------------------------

        public IActionResult Index(ShipsFilter filter,
            ShipsSortingInfo sortingInfo, string sortFieldName)
        {
            filter ??= new();
            sortingInfo ??= new();
            filter.ShipyardNamesList = _unitOfWork.CreateShipyardSelectList(filter.ShipyardName);
            filter.TypeOfShipNamesList = _unitOfWork.CreateTypeOfShipSelectList(filter.TypeOfShipName);
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
                ShipyardSelectList = _unitOfWork.CreateShipyardSelectList(),
                TypeOfShipSelectList = _unitOfWork.CreateTypeOfShipSelectList(),
                ProjectSelectList = _unitOfWork.CreateProjectSelectList()
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

                        TempData["message"] = CreatedMessage(ShipCaption, obj.Key);
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("",
                            CreatingErrorMessage(ShipCaption, ex.Message));
                    }
                }
            }
            model.ShipyardSelectList = _unitOfWork.CreateShipyardSelectList(model.ShipyardName);
            model.TypeOfShipSelectList = _unitOfWork.CreateTypeOfShipSelectList(model.TypeOfShipName);
            model.ProjectSelectList = _unitOfWork.CreateProjectSelectList(model.ProjectName);
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
            model.ShipyardSelectList = _unitOfWork.CreateShipyardSelectList(model.ShipyardName);
            model.TypeOfShipSelectList = _unitOfWork.CreateTypeOfShipSelectList(model.TypeOfShipName);
            model.ProjectSelectList = _unitOfWork.CreateProjectSelectList(model.ProjectName);
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

                        TempData["message"] = UpdatedMessage(ShipCaption, obj.Key);
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("",
                            UpdatingErrorMessage(ShipCaption, ex.Message));
                    }
                }
            }
            model.ShipyardSelectList = _unitOfWork.CreateShipyardSelectList(model.ShipyardName);
            model.TypeOfShipSelectList = _unitOfWork.CreateTypeOfShipSelectList(model.TypeOfShipName);
            model.ProjectSelectList = _unitOfWork.CreateProjectSelectList(model.ProjectName);
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
                TempData["message"] = NotFoundMessage(ShipCaption, model.Id);
            }
            else
            {
                _repository.Delete(model.Id);
                _unitOfWork.Save();
                TempData["message"] = DeletedMessage(ShipCaption, obj.Key);
            }
            return RedirectToAction("Index");
        }
    }
}