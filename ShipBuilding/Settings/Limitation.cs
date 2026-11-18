using System;

namespace ShipBuilding.Settings
{
    public static class Limitation
    {
        //Назва
        public const int NameMinLength = 2;
        public const int NameMaxLength = 50;
        //Опис
        public const int DescriptionMinLength = 3;
        public const int DescriptionMaxLength = 500;
        //        Судно
        //Реєстаційний номер
        public const string RegistrationNumberLimitation = @"^\d{7}$";
        //Дата спуску
        public static readonly DateTime LaunchDateMin = new DateTime(1700, 1, 1);
        public static readonly DateTime ShipBuildDateMax = DateTime.Now;
        //Висота
        public const double HeightOfShipMinLength = 1;
        public const double HeighthOfShipMaxLength = 84;
        //Ширина
        public const double WidthOfShipMinLength = 1;
        public const double WidthOfShipMaxLength = 100;
        //Довжина
        public const double LengthOfShipMinLength = 1;
        public const double LengthOfShipMaxLength = 460;

        //        Завод
        //Назва країна
        public const int CountryNameNameMinLength = 2;
        public const int CountryNameNameMaxLength = 70;
        //Дата заснування
        public const int YearOfFoundedMinLength = 1700;
        //Кількість доків
        public const int NumberOfDocksMinLength = 0;
        //Потужність на рік
        public const double CapacityPerYearMinLength = 0;

        //        Тип судна
        //Код типу
        public const string TypeCodeLimitation = @"^[A-Z]{2,5}$";

        // Код проєкту: SP-РРРР-НН (напр. SP-2024-001)
        public const string ProjectCodeLimitation = @"^SP-\d{4}-\d{3}$";

        public const string RegistrationNumberErrorMessage = "Потрібно ввести 7 цифр";
        public const string TypeCodeErrorMessage = "Потрібно ввести від 2 до 5 символів англійського алфавіту";
    }
}
