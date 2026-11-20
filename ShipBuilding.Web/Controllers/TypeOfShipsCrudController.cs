using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShipBuilding.Services.Interfaces;
using ShipBuilding.Services.TypeOfShips;
using ShipBuilding.Web.ViewModels.TypeOfShips;
using static ShipBuilding.Settings.UiSettings;
using static ShipBuilding.Web.Resources.WebMessages;

namespace ShipBuilding.Web.Controllers
{
    public class TypeOfShipsCrudController : Controller
    {
        private readonly ITypeOfShipsService _types;

        private readonly IMapper _mapper;

        public TypeOfShipsCrudController(ITypeOfShipsService types, IMapper mapper)
        {
            _types = types;
            _mapper = mapper;
        }

        // ---------------------------------------------------------------------------

        public IActionResult Index(TypeOfShipsFilter filter, bool? expand)
        {
            filter ??= new();
            var query = _mapper.Map<TypeOfShipsQuery>(filter);

            // Без фільтра view показує дерево, з фільтром — плоский список у порядку ієрархії
            if (query.IsEmpty)
            {
                var tree = _mapper.Map<List<TypeOfShipBrowsingViewModel>>(_types.GetTree());
                return View(new TypeOfShipSelectionViewModel(filter, tree, [], isFiltered: false, expand));
            }
            var found = _mapper.Map<List<TypeOfShipBrowsingViewModel>>(_types.GetList(query));
            return View(new TypeOfShipSelectionViewModel(filter, [], found, isFiltered: true, expand));
        }

        // ---------------------------------------------------------------------------

        [HttpGet]
        public ActionResult Details(int id)
        {
            var obj = _types.GetById(id);
            if (obj == null) return NotFound();

            return View(_mapper.Map<TypeOfShipEditingViewModel>(obj));
        }

        // ---------------------------------------------------------------------------

        [HttpGet]
        public ActionResult Create() =>
            View(new TypeOfShipEditingViewModel { ParentSelectList = ParentSelectList(null) });

        [HttpPost]
        public ActionResult Create(TypeOfShipEditingViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_types.Create(_mapper.Map<TypeOfShipDto>(model), out string? message))
                {
                    TempData["message"] = $"{DateMessageTemplate} {TypeOfShipCaption.ToLower()} \"{model.Name}\" {CreatedMessageTemplate}";
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", message!);
            }
            model.ParentSelectList = ParentSelectList(model.ParentName);
            return View(model);
        }

        // ---------------------------------------------------------------------------

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = _types.GetById(id);
            if (obj == null) return NotFound();

            var model = _mapper.Map<TypeOfShipEditingViewModel>(obj);
            model.ParentSelectList = ParentSelectList(model.ParentName, excludeId: id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(TypeOfShipEditingViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_types.Update(_mapper.Map<TypeOfShipDto>(model), out string? message))
                {
                    TempData["message"] = $"{DateMessageTemplate} {TypeOfShipCaption.ToLower()} \"{model.Name}\" {UpdatedMessageTemplate}";
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", message!);
            }
            model.ParentSelectList = ParentSelectList(model.ParentName, excludeId: model.Id);
            return View(model);
        }

        // ---------------------------------------------------------------------------

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var obj = _types.GetById(id);
            if (obj == null) return NotFound();

            return View(_mapper.Map<TypeOfShipDeletingViewModel>(obj));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(TypeOfShipDeletingViewModel model)
        {
            var obj = _types.GetById(model.Id);
            if (obj == null)
            {
                TempData["message"] = NotFoundMessage(TypeOfShipCaption, model.Id);
            }
            else if (!_types.Delete(model.Id, out string? message))
            {
                TempData["error"] = message;
                return RedirectToAction("Delete", new { id = model.Id });
            }
            else
            {
                TempData["message"] = $"{DateMessageTemplate} {TypeOfShipCaption.ToLower()} \"{obj.Key}\" {DeletedMessageTemplate}";
            }
            return RedirectToAction("Index");
        }

        // ---------------------------------------------------------------------------

        // Можливі батьківські типи; під час редагування без самого типу та його нащадків
        private SelectList ParentSelectList(string? selectedValue, int? excludeId = null) =>
            new(_types.GetParentCandidates(excludeId), selectedValue);
    }
}
