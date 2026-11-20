namespace ShipBuilding.Services.TypeOfShips
{
    public record TypeOfShipsQuery
    {
        public string? Name { get; init; }
        public string? TypeCode { get; init; }

        public bool IsEmpty => string.IsNullOrWhiteSpace(Name) && string.IsNullOrWhiteSpace(TypeCode);
    }
}
