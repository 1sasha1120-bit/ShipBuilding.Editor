using System.ComponentModel.DataAnnotations;
using ShipBuilding.Statistics.DataQuality;
using static ShipBuilding.Web.Resources.WebTexts;

namespace ShipBuilding.Web.Models.AdminPanelModels
{
    public class AdminPanelModel
    {
        public IReadOnlyList<AdminSection> Sections { get; init; } = [];

        [Display(Name = IncompleteRecordsHeader)]
        public IReadOnlyList<IncompleteRecord> IncompleteRecords { get; init; } = [];
    }
}
