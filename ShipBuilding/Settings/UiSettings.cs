namespace ShipBuilding.Settings
{
    public static partial class UiSettings
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
        public const string RegistrationNumberCaption = "Реєстраційний номер IMO";
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

        // Статуси суден
        public const string StatusUnknownCaption = "Невідомо";
        public const string StatusInServiceCaption = "В експлуатації";
        public const string StatusUnderRepairCaption = "На ремонті";
        public const string StatusInReserveCaption = "В резерві";
        public const string StatusDecommissionedCaption = "Списано";

        // Головна сторінка (dashboard)
        public const string ShipsByStatusHeader = "Судна за статусом";
        public const string TopShipyardsHeader = "Найбільші заводи";
        public const string LatestShipsHeader = "Останні спущені на воду";
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
        // — проєкти без суден
        public const string ProjectsWithoutShipsHeader = "Проєкти без суден";
        public const string ProjectCodeCaption = "Код проєкту";

        // Налаштування інформації про засоби введення-виведення
        public const string XmlFileTypeCaption = "Файли формату XML";
        public const string XmlFileExtension = ".xml";
        public const string XmlRootElementName = "ShipBuilding";
    }
}
