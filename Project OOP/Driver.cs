using System;

namespace TaxiDispatcher
{
    public class Driver
    {
        public string FullName { get; set; }
        public string CarModel { get; set; }
        public string LicensePlate { get; }
        public bool IsAvailable { get; set; } // Поле стану водія

        private Driver()
        {
            FullName = "Системний водій";
            CarModel = "Службове авто";
            LicensePlate = "AA0000AA";
            IsAvailable = false;
        }

        public Driver(string fullName, string carModel, string licensePlate)
        {
            FullName = fullName;
            CarModel = carModel;
            LicensePlate = licensePlate;
            IsAvailable = true; // За замовчуванням водій вільний і готовий до роботи
        }

        public Driver(string fullName, string licensePlate) : this(fullName, "Стандартне авто", licensePlate) { }

        public static Driver CreateSystemDriver() => new Driver();

        // --- ПРЕДИКАТНА ФУНКЦІЯ ---
        // Перевіряє, чи активний водій і чи може він взяти замовлення прямо зараз
        public bool IsReadyForOrder()
        {
            return IsAvailable && !string.IsNullOrEmpty(FullName);
        }

        // Методи зміни стану
        public void ToggleAvailability(bool status)
        {
            IsAvailable = status;
        }
    }
}