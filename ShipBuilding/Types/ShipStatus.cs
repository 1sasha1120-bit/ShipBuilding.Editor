using System.ComponentModel.DataAnnotations;

namespace ShipBuilding.Types
{
    public enum ShipStatus
    {
        [Display(Name = "Невідомо")]
        Unknown = 0,

        [Display(Name = "В експлуатації")]
        InService = 1,

        [Display(Name = "На ремонті")]
        UnderRepair = 2,

        [Display(Name = "В резерві")]
        InReserve = 3,

        [Display(Name = "Списано")]
        Decommissioned = 4
    }
}
