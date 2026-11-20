using AutoMapper;
using Common.Types;
using Microsoft.AspNetCore.Mvc;
using ShipBuilding.Services.Interfaces;
using ShipBuilding.Services.Shipyards;
using ShipBuilding.Web.ViewModels.Shipyards;
using static ShipBuilding.Settings.UiSettings;
using static ShipBuilding.Web.Resources.WebMessages;

namespace ShipBuilding.Web.Controllers
{
    public class ShipyardsCrudController : Controller
    {
        private readonly IShipyardsService _shipyards;
        private readonly IMapper _mapper;

        public ShipyardsCrudController(IShipyardsService shipyards, IMapper mapper)
        {
            _shipyards = shipyards;
            _mapper = mapper;
        }

        // ---------------------------------------------------------------------------

        public IActionResult Index(ShipyardsFilter filter,
            ShipyardSortField sortBy = ShipyardSortField.Name, SortingOrder order = SortingOrder.Ascending)
        {
            filter ??= new();
            var query = _mapper.Map<ShipyardsQuery>(filter) with { SortBy = sortBy, Order = order };
            var shipyards = _mapper.Map<List<ShipyardBrowsingViewModel>>(_shipyards.GetList(query));
            return View(new ShipyardSelectionViewModel(filter, sortBy, order, shipyards));
        }

        // ---------------------------------------------------------------------------

        [HttpGet]
        public ActionResult Details(int id)
        {
            var obj = _shipyards.GetById(id);
            if (obj == null) return NotFound();

            return View(_mapper.Map<ShipyardEditingViewModel>(obj));
        }

        // ---------------------------------------------------------------------------

        [HttpGet]
        public ActionResult Create() => View(new ShipyardEditingViewModel());

        [HttpPost]
        public ActionResult Create(ShipyardEditingViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_shipyards.Create(_mapper.Map<ShipyardDto>(model), out string? message))
                {
                    TempData["message"] = $"{DateMessageTemplate} {ShipyardCaption.ToLower()} \"{model.Name}\" {CreatedMessageTemplate}";
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", message!);
            }
            return View(model);
        }

        // ---------------------------------------------------------------------------

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = _shipyards.GetById(id);
            if (obj == null) return NotFound();
            return View(_mapper.Map<ShipyardEditingViewModel>(obj));
        }

        [HttpPost]
        public ActionResult Edit(ShipyardEditingViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_shipyards.Update(_mapper.Map<ShipyardDto>(model), out string? message))
                {
                    TempData["message"] = $"{DateMessageTemplate} {ShipyardCaption.ToLower()} \"{model.Name}\" {UpdatedMessageTemplate}";
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", message!);
            }
            return View(model);
        }

        // ---------------------------------------------------------------------------

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var obj = _shipyards.GetById(id);
            if (obj == null) return NotFound();
            return View(_mapper.Map<ShipyardDeletingViewModel>(obj));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ShipyardDeletingViewModel model)
        {
            var obj = _shipyards.GetById(model.Id);
            if (obj == null)
            {
                TempData["message"] = NotFoundMessage(ShipyardCaption, model.Id);
            }
            else if (!_shipyards.Delete(model.Id, out string? message))
            {
                TempData["error"] = message;
                return RedirectToAction("Delete", new { id = model.Id });
            }
            else
            {
                TempData["message"] = $"{DateMessageTemplate} {ShipyardCaption.ToLower()} \"{obj.Key}\" {DeletedMessageTemplate}";
            }
            return RedirectToAction("Index");
        }
    }
}
