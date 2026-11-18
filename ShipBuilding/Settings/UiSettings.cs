namespace ShipBuilding.Settings
{
    public static class UiSettings
    {
        // Налаштування інформації про ПО
        public const string SubjectAreaTitle = "ПО \"Суднобудування\"";
        public const string ShortSubjectAreaTitle = "Суднобудування";

        // Налаштування інформації про сутності ПО
        public const string ShipCaption = "Судно";
        public const string ShipyardCaption = "Завод";
        public const string TypeOfShipCaption = "Тип судна";
        public const string ProjectCaption = "Проєкт судна";
        public const string ShipsHeader = "Судна";
        public const string ShipyardsHeader = "Заводи";
        public const string TypeOfShipsHeader = "Типи суден";
        public const string ProjectsHeader = "Проєкти суден";

        // Налаштування інформації про атрибути сутностей ПО
        public const string IdCaption = "№";
        public const string NameCaption = "Назва";
        public const string DescriptionCaption = "Опис";
        //Судна
        public const string RegistrationNumberCaption = "Реєстаційний номер IMO";
        public const string LaunchDateCaption = "Дата спуску на воду";
        public const string StatusCaption = "Статус";
        public const string HeightCaption = "Висота, м";
        public const string WidthCaption = "Ширина, м";
        public const string LengthCaption = "Довжина, м";

        //Суднобудівні заводи
        public const string CountryNameCaption = "Назва країна";
        public const string YearOfFoundedCaption = "Рік заснування";
        public const string NumberOfDocksCaption = "Кількість доків";
        public const string CapacityPerYearCaption = "Річна потужність";
        //Типи суден
        public const string ParentCaption = "Тип вищого рівня";
        public const string TypeCodeCaption = "Код типу";

        // Головна сторінка (dashboard)
        public const string HomePageTitle = "Головна";
        public const string DashboardSubtitle = "Зведена інформація по базі даних";
        public const string AdminPanelCaption = "Адмін панель";
        public const string ShipsByStatusHeader = "Судна за статусом";
        public const string TopShipyardsHeader = "Найбільші заводи";
        public const string LatestShipsHeader = "Останні спущені на воду";
        public const string CountCaption = "Кількість";
        public const string PercentCaption = "Частка, %";
        public const string CountryCaption = "Країна";
        public const string ShipsCountCaption = "Суден";
        public const string ProjectsCountCaption = "Проєктів";
        public const string ShortLaunchDateCaption = "Дата спуску";
        // — бюджет проєктів
        public const string BudgetHeader = "Бюджет проєктів";
        public const string BudgetCaption = "Бюджет";
        public const string TotalBudgetCaption = "Загальний бюджет";
        public const string AverageBudgetCaption = "Середній бюджет";
        public const string ProjectsWithBudgetCaption = "Проєктів з бюджетом";
        public const string MostExpensiveProjectCaption = "Найдорожчий проєкт";
        public const string NoBudgetsMessage = "Жоден проєкт не має бюджету";
        // — проєкти без суден
        public const string ProjectsWithoutShipsHeader = "Проєкти без суден";
        public const string ProjectCodeCaption = "Код проєкту";
        public const string AllProjectsHaveShipsMessage = "Для кожного проєкту вже є судна";
        // — судна за категоріями (типи верхнього рівня)
        public const string ShipsByCategoryHeader = "Судна за категоріями";
        public const string CategoryCaption = "Категорія";
        public const string WithoutTypeCaption = "Без типу";
        public const string NoShipsMessage = "Суден ще немає";

        // Адмін панель
        public const string AdminPanelSubtitle = "Редагування довідників і реєстрів";
        public const string HomePageCaption = "Головна";
        public const string OpenListCaption = "Відкрити";
        public const string RecordsCountCaption = "Записів";
        public const string TypeOfShipsDescription = "Ієрархія та класифікація";
        public const string ShipyardsDescription = "Суднобудівні підприємства";
        public const string ProjectsDescription = "Проєкти суден і бюджети";
        public const string ShipsDescription = "Реєстр суден";
        public const string IncompleteRecordsHeader = "Потребують заповнення";
        public const string EntityCaption = "Розділ";
        public const string MissingFieldsCaption = "Не заповнено";
        public const string AllRecordsCompleteMessage = "Усі записи заповнені";

        // Контролери
        public const string TypesController = "TypeOfShipsCrud";
        public const string ShipyardsController = "ShipyardsCrud";
        public const string ProjectsController = "ShipProjectsCrud";
        public const string ShipsController = "ShipsCrud";

        // Налаштування інформації про засоби введення-виведення
        public const string XmlFileTypeCaption = "Файли формату XML";
        public const string XmlFileExtension = ".xml";
        public const string XmlRootElementName = "ShipBuilding";
    }
}
