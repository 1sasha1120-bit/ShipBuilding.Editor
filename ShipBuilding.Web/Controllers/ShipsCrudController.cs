using AutoMapper;
using Common.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShipBuilding.Services.Interfaces;
using ShipBuilding.Services.Ships;
using ShipBuilding.Web.ViewModels.Ships;
using static ShipBuilding.Settings.UiSettings;
using static ShipBuilding.Web.Resources.WebMessages;

namespace ShipBuilding.Web.Controllers
{
    public class ShipsCrudController : Controller
    {
        private readonly IShipsService _ships;

        // Для випадних списків верфей, типів і проєктів
        private readonly IShipyardsService _shipyards;
        private readonly ITypeOfShipsService _types;
        private readonly IShipProjectsService _projects;

        private readonly IMapper _mapper;

        public ShipsCrudController(IShipsService ships, IShipyardsService shipyards,
            ITypeOfShipsService types, IShipProjectsService projects, IMapper mapper)
        {
            _ships = ships;
            _shipyards = shipyards;
            _types = types;
            _projects = projects;
            _mapper = mapper;
        }

        // ---------------------------------------------------------------------------

        public IActionResult Index(ShipsFilter filter,
            ShipSortField sortBy = ShipSortField.Name, SortingOrder order = SortingOrder.Ascending)
        {
            filter ??= new();
            filter.ShipyardNamesList = new SelectList(_shipyards.GetNames(), filter.ShipyardName);
            filter.TypeOfShipNamesList = new SelectList(_types.GetNames(), filter.TypeOfShipName);
            var query = _mapper.Map<ShipsQuery>(filter) with { SortBy = sortBy, Order = order };
            var ships = _mapper.Map<List<ShipBrowsingViewModel>>(_ships.GetList(query));
            return View(new ShipSelectionViewModel(filter, sortBy, order, ships));
        }

        // ---------------------------------------------------------------------------

        [HttpGet]
        public IActionResult Details(int id)
        {
            var ship = _ships.GetById(id);
            if (ship == null) return NotFound();

            return View(_mapper.Map<ShipEditingViewModel>(ship));
        }

        // ---------------------------------------------------------------------------

        [HttpGet]
        public IActionResult Create() => View(FillSelectLists(new ShipEditingViewModel()));

        [HttpPost]
        public IActionResult Create(ShipEditingViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_ships.Create(_mapper.Map<ShipDto>(model), out string? message))
                {
                    TempData["message"] = $"{DateMessageTemplate} {ShipCaption.ToLower()} \"{model.Name}\" {CreatedMessageTemplate}";
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", message!);
            }
            return View(FillSelectLists(model));
        }

        // ---------------------------------------------------------------------------

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var ship = _ships.GetById(id);
            if (ship == null) return NotFound();

            return View(FillSelectLists(_mapper.Map<ShipEditingViewModel>(ship)));
        }

        [HttpPost]
        public IActionResult Edit(ShipEditingViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_ships.Update(_mapper.Map<ShipDto>(model), out string? message))
                {
                    TempData["message"] = $"{DateMessageTemplate} {ShipCaption.ToLower()} \"{model.Name}\" {UpdatedMessageTemplate}";
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", message!);
            }
            return View(FillSelectLists(model));
        }

        // ---------------------------------------------------------------------------

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var ship = _ships.GetById(id);
            if (ship == null) return NotFound();

            return View(_mapper.Map<ShipDeletingViewModel>(ship));
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // для захисту від CSRF-атак
        public IActionResult Delete(ShipDeletingViewModel model)
        {
            var obj = _ships.GetById(model.Id);
            if (obj == null)
            {
                TempData["message"] = NotFoundMessage(ShipCaption, model.Id);
            }
            else if (!_ships.Delete(model.Id, out string? message))
            {
                TempData["error"] = message;
                return RedirectToAction("Delete", new { id = model.Id });
            }
            else
            {
                TempData["message"] = $"{DateMessageTemplate} {ShipCaption.ToLower()} \"{obj.Key}\" {DeletedMessageTemplate}";
            }
            return RedirectToAction("Index");
        }

        // ---------------------------------------------------------------------------

        private ShipEditingViewModel FillSelectLists(ShipEditingViewModel model)
        {
            model.ShipyardSelectList = new SelectList(_shipyards.GetNames(), model.ShipyardName);
            model.TypeOfShipSelectList = new SelectList(_types.GetNames(), model.TypeOfShipName);
            model.ProjectSelectList = new SelectList(_projects.GetNames(), model.ProjectName);
            return model;
        }
    }
}
