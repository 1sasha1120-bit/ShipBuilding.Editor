using ShipBuilding.Services.TypeOfShips;

namespace ShipBuilding.Services.Interfaces
{
    /// <summary>
    /// Операції над типами суден. GetList повертає типи пласким списком у порядку ієрархії
    /// (батьківський тип, за ним його підтипи) з рівнем Depth. Видаляється тип, лише якщо
    /// в нього немає підтипів і на нього не посилаються судна.
    /// </summary>
    public interface ITypeOfShipsService : IEntityService<TypeOfShipDto, TypeOfShipsQuery>
    {
        /// <summary>Дерево типів: кореневі типи з підтипами (Children) на кожному рівні.</summary>
        IReadOnlyList<TypeOfShipDto> GetTree();

        /// <summary>Назви типів за абеткою — для випадних списків.</summary>
        IReadOnlyList<string> GetNames();

        /// <summary>
        /// Назви типів, які можна призначити батьківським. Якщо задано <paramref name="excludeId"/>,
        /// з переліку виключаються цей тип і всі його нащадки (щоб не утворити цикл).
        /// </summary>
        IReadOnlyList<string> GetParentCandidates(int? excludeId = null);
    }
}
