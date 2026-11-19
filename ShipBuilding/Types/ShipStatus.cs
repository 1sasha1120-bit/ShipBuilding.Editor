using System.ComponentModel.DataAnnotations;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Types
{
    public enum ShipStatus
    {
        [Display(Name = StatusUnknownCaption)]
        Unknown = 0,

        [Display(Name = StatusInServiceCaption)]
        InService = 1,

        [Display(Name = StatusUnderRepairCaption)]
        UnderRepair = 2,

        [Display(Name = StatusInReserveCaption)]
        InReserve = 3,

        [Display(Name = StatusDecommissionedCaption)]
        Decommissioned = 4
    }
}
