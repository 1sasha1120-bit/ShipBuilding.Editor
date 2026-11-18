using System.ComponentModel.DataAnnotations;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Web.Models.AdminPanelModels
{
    public class AdminPanelModel
    {
        public IReadOnlyList<AdminSection> Sections { get; init; } = [];

        [Display(Name = IncompleteRecordsHeader)]
        public IReadOnlyList<IncompleteRecord> IncompleteRecords { get; init; } = [];
    }
}
