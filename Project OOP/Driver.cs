using System;

namespace TaxiDispatcher
{
    public class Driver
    {
        // Відкрита властивість читання та запису з валідацією
        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set { fullName = !string.IsNullOrWhiteSpace(value) ? value : "Ім'я відсутнє"; }
        }

        public string CarModel { get; set; }

        // Властивість тільки для читання (Read-only)
        public string LicensePlate { get; }

        // 4. Закритий конструктор (Private constructor)
        // Зазвичай використовується, щоб заборонити створення об'єктів ззовні без параметрів
        private Driver()
        {
            FullName = "Системний водій";
            CarModel = "Службове авто";
            LicensePlate = "AA0000AA";
        }

        // 2. Конструктор з параметрами
        public Driver(string fullName, string carModel, string licensePlate)
        {
            FullName = fullName;
            CarModel = carModel;
            LicensePlate = licensePlate;
            Console.WriteLine($"[Конструктор з параметрами]: Зареєстровано водія {FullName} на {CarModel}");
        }

        // 6. Конструктор, що викликає інший конструктор (Перевантаження для експрес-реєстрації)
        public Driver(string fullName, string licensePlate) : this(fullName, "Стандартне авто", licensePlate)
        {
            Console.WriteLine("[Конструктор з викликом іншого]: Водію присвоєно стандартне авто.");
        }

        // Фабричний метод, який використовує закритий конструктор
        public static Driver CreateSystemDriver()
        {
            return new Driver();
        }
    }
}