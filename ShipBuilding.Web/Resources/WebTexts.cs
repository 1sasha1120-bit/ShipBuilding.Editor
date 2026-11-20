using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Web.Resources
{
    public static class WebTexts
    {
        // Головна сторінка (dashboard)
        public const string HomePageTitle = "Головна";
        public const string DashboardSubtitle = "Зведена інформація";
        public const string NoBudgetsMessage = "Жоден проєкт не має бюджету";
        public const string AllProjectsHaveShipsMessage = "Для кожного проєкту вже є судна";
        public const string NoShipsMessage = "Суден ще немає";

        // Перегляд довідників і реєстрів з головної сторінки (без редагування)
        public const string BrowseSubtitle = "Перегляд";
        public const string BackToHomeCaption = "← Головна";
        public const string NoRecordsMessage = "Записів ще немає";

        // Адмін панель
        public const string AdminPanelCaption = "Адмін панель";
        public const string AdminPanelSubtitle = "Редагування довідників і реєстрів";
        public const string HomePageCaption = "Головна";
        public const string OpenListCaption = "Відкрити";
        public const string TypeOfShipsDescription = "Ієрархія та класифікація";
        public const string ShipyardsDescription = "Суднобудівні підприємства";
        public const string ProjectsDescription = "Проєкти суден і бюджети";
        public const string ShipsDescription = "Реєстр суден";

        // Додаткові назви сутностей
        public const string ShortProjectCaption = "Проєкт";
        public const string FullShipyardCaption = "Суднобудівний завод";
        public const string FullShipyardsHeader = "Суднобудівні заводи";

        // Короткі підписи атрибутів (заголовки колонок, картки)
        public const string ShortRegistrationNumberCaption = "Реєстраційний номер";
        public const string ShortTypeCaption = "Тип";
        public const string ShortCodeCaption = "Код";
        public const string ShortYearCaption = "Рік";
        public const string ShortDocksCaption = "Доків";
        public const string ShortCapacityCaption = "Потужність";
        public const string BudgetUsdCaption = "Бюджет, USD";
        public const string TonnesUnit = "т";

        // Кнопки та посилання
        public const string CreateCaption = "Створити";
        public const string SaveCaption = "Зберегти";
        public const string EditCaption = "Редагувати";
        public const string DeleteCaption = "Видалити";
        public const string ConfirmDeleteCaption = "Так, видалити";
        public const string DetailsCaption = "Деталі";
        public const string CancelCaption = "Скасувати";
        public const string SearchCaption = "Пошук";
        public const string ClearCaption = "Очистити";
        public const string BackToListCaption = "← Назад до списку";
        public const string BackToAdminCaption = "← Адмін";
        public const string CreateFirstCaption = "Створити перший →";
        public const string ExpandAllCaption = "Розгорнути всі";
        public const string CollapseAllCaption = "Згорнути всі";
        public const string TypesHierarchyCaption = "Ієрархія типів:";

        // Фільтри: межі діапазону
        public const string RangeFromCaption = "від";
        public const string RangeToCaption = "до";

        // Пункти випадаючих списків
        public const string SelectShipyardOption = "— Оберіть завод —";
        public const string SelectTypeOfShipOption = "— Оберіть тип —";
        public const string NoProjectOption = "— Без проєкту —";
        public const string NoParentOption = "— Без батьківського типу —";
        public const string AllShipyardsOption = "-- Всі заводи --";
        public const string AllTypesOfShipsOption = "-- Всі типи суден --";

        // Підзаголовки сторінок
        public const string FillFormSubtitle = "Заповніть поля та збережіть";
        public const string DetailsSubtitle = "детальна інформація";
        public const string ShipDetailsSubtitle = $"{ShipCaption} · {DetailsSubtitle}";
        public const string ShipyardDetailsSubtitle = $"{FullShipyardCaption} · {DetailsSubtitle}";
        public const string TypeOfShipDetailsSubtitle = $"{TypeOfShipCaption} · {DetailsSubtitle}";
        public const string ProjectDetailsSubtitle = $"{ProjectCaption} · {DetailsSubtitle}";

        // Заголовки вкладок браузера
        public const string DetailsTitleSuffix = "деталі";
        public const string EditingTitleSuffix = "редагування";
        public const string DeletingTitleSuffix = "видалення";

        public const string ShipDetailsTitle = $"{ShipCaption} — {DetailsTitleSuffix}";
        public const string ShipEditTitle = $"{ShipCaption} — {EditingTitleSuffix}";
        public const string ShipDeleteTitle = $"{ShipCaption} — {DeletingTitleSuffix}";
        public const string ShipyardDetailsTitle = $"{ShipyardCaption} — {DetailsTitleSuffix}";
        public const string ShipyardEditTitle = $"{ShipyardCaption} — {EditingTitleSuffix}";
        public const string ShipyardDeleteTitle = $"{ShipyardCaption} — {DeletingTitleSuffix}";
        public const string TypeOfShipDetailsTitle = $"{TypeOfShipCaption} — {DetailsTitleSuffix}";
        public const string TypeOfShipEditTitle = $"{TypeOfShipCaption} — {EditingTitleSuffix}";
        public const string TypeOfShipDeleteTitle = $"{TypeOfShipCaption} — {DeletingTitleSuffix}";
        public const string ProjectDetailsTitle = $"{ShortProjectCaption} — {DetailsTitleSuffix}";
        public const string ProjectEditTitle = $"{ShortProjectCaption} — {EditingTitleSuffix}";
        public const string ProjectDeleteTitle = $"{ShortProjectCaption} — {DeletingTitleSuffix}";

        // Заголовки сторінок створення / редагування / видалення
        public const string NewShipHeader = "Нове судно";
        public const string NewShipyardHeader = "Новий завод";
        public const string NewTypeOfShipHeader = "Новий тип судна";
        public const string NewProjectTitle = "Новий проєкт";
        public const string NewProjectHeader = "Новий проєкт судна";

        public const string EditShipHeader = "Редагування судна";
        public const string EditShipyardHeader = "Редагування заводу";
        public const string EditTypeOfShipHeader = "Редагування типу судна";
        public const string EditProjectHeader = "Редагування проєкту";

        public const string DeleteShipHeader = "Видалення судна";
        public const string DeleteShipyardHeader = "Видалення заводу";
        public const string DeleteTypeOfShipHeader = "Видалення типу судна";
        public const string DeleteProjectHeader = "Видалення проєкту";

        public const string DeleteWarningMessage = "Цю дію неможливо скасувати. Запис буде видалено назавжди.";

        // Порожні дані
        public const string NothingFoundMessage = "Нічого не знайдено";
        public const string NoProjectsFoundMessage = "Проєктів не знайдено";
        public const string NoShipyardsMessage = "Заводів ще немає";
        public const string NoLaunchedShipsMessage = "Немає суден з датою спуску";
        public const string NoTypesOfShipsMessage = "Немає жодного типу судна.";
    }
}
