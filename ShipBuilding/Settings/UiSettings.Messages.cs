namespace ShipBuilding.Settings
{
    public static partial class UiSettings
    {
        // Перевірка цілісності даних
        public static string DuplicateNameMessage(string entityCaption, string? name)
        {
            return $"Помилка: вже існує запис про {entityCaption.ToLower()} з назвою \"{name}\"";
        }

        public static string DuplicateRegistrationNumberMessage(string entityCaption, string? number)
        {
            return $"Помилка: вже існує запис про {entityCaption.ToLower()} з номером \"{number}\"";
        }

        public static string AndRegistrationNumberMessage(string? number)
        {
            return $" та номером \"{number}\"";
        }

        public static string HierarchyCycleMessage(string entityCaption)
        {
            return $"Помилка: неможливо призначити батьківський {entityCaption.ToLower()}, " +
                "оскільки він є самим вузлом або його нащадком (цикл у ієрархії)";
        }

        public static string DeletingForbiddenMessage(string entityCaption, string? key,
            string dependentHeader)
        {
            return $"Видалення заборонено, оскільки для {entityCaption.ToLower()} {key} " +
                $"є дані про підпорядковані {dependentHeader.ToLower()}";
        }

        public static string AndDependentMessage(string dependentHeader)
        {
            return $" та підпорядковані {dependentHeader.ToLower()}";
        }

        // Пошук записів
        public static string NotFoundMessage(string entityCaption, object? id)
        {
            return $"Дані про {entityCaption.ToLower()} \"{id}\" відсутні у сховищі";
        }

        public static string ReferenceNotFoundMessage(string entityCaption, string? name)
        {
            return $"Помилка: відсутній запис про {entityCaption.ToLower()} з назвою \"{name}\"";
        }
    }
}
