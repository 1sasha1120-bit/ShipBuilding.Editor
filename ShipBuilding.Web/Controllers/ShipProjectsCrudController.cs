using AutoMapper;
using Common.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShipBuilding.Services.Interfaces;
using ShipBuilding.Services.ShipProjects;
using ShipBuilding.Web.ViewModels.ShipProjects;
using static ShipBuilding.Settings.UiSettings;
using static ShipBuilding.Web.Resources.WebMessages;

namespace ShipBuilding.Web.Controllers
{
    public class ShipProjectsCrudController : Controller
    {
        private readonly IShipProjectsService _projects;

        // Для випадного списку верфей
        private readonly IShipyardsService _shipyards;

        private readonly IMapper _mapper;

        public ShipProjectsCrudController(IShipProjectsService projects, IShipyardsService shipyards,
            IMapper mapper)
        {
            _projects = projects;
            _shipyards = shipyards;
            _mapper = mapper;
        }

        // ---------------------------------------------------------------------------

        public IActionResult Index(ShipProjectsFilter filter,
            ShipProjectSortField sortBy = ShipProjectSortField.Name, SortingOrder order = SortingOrder.Ascending)
        {
            filter ??= new();
            filter.ShipyardNamesList = new SelectList(_shipyards.GetNames(), filter.ShipyardName);
            var query = _mapper.Map<ShipProjectsQuery>(filter) with { SortBy = sortBy, Order = order };
            var projects = _mapper.Map<List<ShipProjectBrowsingViewModel>>(_projects.GetList(query));
            return View(new ShipProjectSelectionViewModel(filter, sortBy, order, projects));
        }

        // ---------------------------------------------------------------------------

        [HttpGet]
        public ActionResult Details(int id)
        {
            var shipProject = _projects.GetById(id);
            if (shipProject == null) return NotFound();

            return View(_mapper.Map<ShipProjectEditingViewModel>(shipProject));
        }

        // ---------------------------------------------------------------------------

        [HttpGet]
        public ActionResult Create() => View(FillSelectLists(new ShipProjectEditingViewModel()));

        [HttpPost]
        public ActionResult Create(ShipProjectEditingViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_projects.Create(_mapper.Map<ShipProjectDto>(model), out string? message))
                {
                    TempData["message"] = $"{DateMessageTemplate} {ProjectCaption.ToLower()} \"{model.Name}\" {CreatedMessageTemplate}";
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", message!);
            }
            return View(FillSelectLists(model));
        }

        // ---------------------------------------------------------------------------

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = _projects.GetById(id);
            if (obj == null) return NotFound();

            return View(FillSelectLists(_mapper.Map<ShipProjectEditingViewModel>(obj)));
        }

        [HttpPost]
        public ActionResult Edit(ShipProjectEditingViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_projects.Update(_mapper.Map<ShipProjectDto>(model), out string? message))
                {
                    TempData["message"] = $"{DateMessageTemplate} {ProjectCaption.ToLower()} \"{model.Name}\" {UpdatedMessageTemplate}";
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", message!);
            }
            return View(FillSelectLists(model));
        }

        // ---------------------------------------------------------------------------

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var obj = _projects.GetById(id);
            if (obj == null) return NotFound();

            return View(_mapper.Map<ShipProjectBrowsingViewModel>(obj));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ShipProjectBrowsingViewModel model)
        {
            var obj = _projects.GetById(model.Id);
            if (obj == null)
            {
                TempData["message"] = NotFoundMessage(ProjectCaption, model.Id);
            }
            else if (!_projects.Delete(model.Id, out string? message))
            {
                TempData["error"] = message;
                return RedirectToAction("Delete", new { id = model.Id });
            }
            else
            {
                TempData["message"] = $"{DateMessageTemplate} {ProjectCaption.ToLower()} \"{obj.Key}\" {DeletedMessageTemplate}";
            }
            return RedirectToAction("Index");
        }

        // ---------------------------------------------------------------------------

        private ShipProjectEditingViewModel FillSelectLists(ShipProjectEditingViewModel model)
        {
            model.ShipyardSelectList = new SelectList(_shipyards.GetNames(), model.ShipyardName);
            return model;
        }
    }
}
