using ShipBuilding.Entities;

namespace ShipBuilding.Services.TypeOfShips
{
    /// <summary>
    /// Дані типу судна: їх повертає сервіс і їх же він приймає для створення чи зміни.
    /// Батьківський тип задається назвою. Key, ParentId, Depth і Children заповнюються
    /// лише у відповідях сервісу.
    /// </summary>
    public record TypeOfShipDto
    {
        public int Id { get; init; }
        public string Key { get; init; } = "";
        public string? Name { get; init; }
        public string? TypeCode { get; init; }
        public string? Description { get; init; }

        /// <summary>Ідентифікатор батьківського типу; null — кореневий тип.</summary>
        public int? ParentId { get; init; }
        public string? ParentName { get; init; }

        /// <summary>Рівень в ієрархії (0 — кореневий); задається, коли тип читається як частина дерева.</summary>
        public int Depth { get; init; }

        /// <summary>Підтипи; заповнюються лише в дереві (<see cref="Interfaces.ITypeOfShipsService.GetTree"/>).</summary>
        public IReadOnlyList<TypeOfShipDto> Children { get; init; } = [];

        internal static TypeOfShipDto From(TypeOfShip type)
        {
            return new()
            {
                Id          = type.Id,
                Key         = type.Key,
                Name        = type.Name,
                TypeCode    = type.TypeCode,
                Description = type.Description,
                ParentId    = type.Parent?.Id,
                ParentName  = type.Parent?.Name
            };
        }
    }
}
