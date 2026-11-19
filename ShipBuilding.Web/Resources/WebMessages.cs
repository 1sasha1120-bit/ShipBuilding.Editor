namespace ShipBuilding.Web.Resources
{
    public static class WebMessages
    {
        // Повідомлення валідації (шаблони DataAnnotations:
        // {0} - назва поля; StringLength: {1} - макс., {2} - мін.; Range: {1} - мін., {2} - макс.)
        public const string RequiredFieldMessage = "Потрібно заповнити поле '{0}'";
        public const string RequiredSelectionMessage = "Потрібно обрати '{0}'";
        public const string StringLengthMessage = "Поле '{0}' повинно містити від {2} до {1} символів";
        public const string RangeMessage = "Поле '{0}' повинно бути в межах від {1} до {2}";
        public const string NonNegativeMessage = "Поле '{0}' не може бути від'ємним";
        public const string FutureDateMessage = "Дата не може бути в майбутньому";
        public const string LaunchDateInFutureMessage = "Дата спуску на воду не може перевищувати сьогоднішню";
        public const string ProjectCodeErrorMessage = "Формат: SP-РРРР-НН (напр. SP-2024-001)";

        // Результати CRUD-операцій
        public const string CreatedMessageTemplate = "додано";
        public const string UpdatedMessageTemplate = "змінено";
        public const string DeletedMessageTemplate = "видалено";
        public const string NotFoundMessageTemplate = "відсутні у сховищі";
        public const string CreatingErrorMessageTemplate = "Помилка створення запису про";
        public const string UpdatingErrorMessageTemplate = "Помилка редагування запису про";
        public const string DateMessageTemplate = "Дані про";

        public static string CreatedMessage(string entityCaption, string? key) =>
            $"{DateMessageTemplate} {entityCaption.ToLower()} \"{key}\" {CreatedMessageTemplate}";

        public static string UpdatedMessage(string entityCaption, string? key) =>
            $"{DateMessageTemplate} {entityCaption.ToLower()} \"{key}\" {UpdatedMessageTemplate}";

        public static string DeletedMessage(string entityCaption, string? key) =>
            $"{DateMessageTemplate} {entityCaption.ToLower()} \"{key}\" {DeletedMessageTemplate}";

        public static string NotFoundMessage(string entityCaption, object? id) =>
            $"{DateMessageTemplate} {entityCaption.ToLower()} \"{id}\" {NotFoundMessageTemplate}";

        public static string CreatingErrorMessage(string entityCaption, string error) =>
            $"{CreatingErrorMessageTemplate} {entityCaption.ToLower()}: {error}";

        public static string UpdatingErrorMessage(string entityCaption, string error) =>
            $"{UpdatingErrorMessageTemplate} {entityCaption.ToLower()}: {error}";
    }
}
