namespace ShipBuilding.Services.Interfaces
{
    /// <summary>
    /// Спільні операції сервісу сутності. Методи запису повертають true в разі успіху;
    /// інакше в <c>message</c> — пояснення (порушене правило або збій збереження).
    /// </summary>
    /// <typeparam name="TDto">Дані сутності, які сервіс повертає і приймає.</typeparam>
    /// <typeparam name="TQuery">Критерії пошуку для списку.</typeparam>
    public interface IEntityService<TDto, TQuery>
        where TDto : class
    {
        /// <summary>Запис за ідентифікатором або null, якщо його немає.</summary>
        TDto? GetById(int id);

        /// <summary>Записи, що відповідають критеріям, у заданому порядку.</summary>
        IReadOnlyList<TDto> GetList(TQuery query);

        bool Create(TDto dto, out string? message);

        bool Update(TDto dto, out string? message);

        bool Delete(int id, out string? message);
    }
}
