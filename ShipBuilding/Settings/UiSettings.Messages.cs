namespace ShipBuilding.Settings
{
    public static partial class UiSettings
    {
        // Перевірка цілісності даних
        public static string DuplicateNameMessage(string entityCaption, string? name) =>
            $"Помилка: вже існує запис про {entityCaption.ToLower()} з назвою \"{name}\"";

        public static string DuplicateRegistrationNumberMessage(string entityCaption, string? number) =>
            $"Помилка: вже існує запис про {entityCaption.ToLower()} з номером \"{number}\"";

        public static string AndRegistrationNumberMessage(string? number) =>
            $" та номером \"{number}\"";

        public static string HierarchyCycleMessage(string entityCaption) =>
            $"Помилка: неможливо призначити батьківський {entityCaption.ToLower()}, " +
            "оскільки він є самим вузлом або його нащадком (цикл у ієрархії)";

        public static string DeletingForbiddenMessage(string entityCaption, string? key,
            string dependentHeader) =>
            $"Видалення заборонено, оскільки для {entityCaption.ToLower()} {key} " +
            $"є дані про підпорядковані {dependentHeader.ToLower()}";

        public static string AndDependentMessage(string dependentHeader) =>
            $" та підпорядковані {dependentHeader.ToLower()}";
    }
}
