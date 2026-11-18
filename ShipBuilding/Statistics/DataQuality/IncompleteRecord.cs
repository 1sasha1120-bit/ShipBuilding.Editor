using System.ComponentModel.DataAnnotations;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Statistics.DataQuality
{
    public record IncompleteRecord(
        EntityKind Kind,
        [property: Display(Name = EntityCaption)] string Section,
        int Id,
        [property: Display(Name = NameCaption)] string? Name,
        [property: Display(Name = MissingFieldsCaption)] IReadOnlyList<string> MissingFields);
}
