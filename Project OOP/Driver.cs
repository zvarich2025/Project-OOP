using System;

namespace TaxiDispatcher
{
    public class Driver
    {
        // Поля (Властивості)
        public string FullName { get; set; }
        public string CarModel { get; set; }
        public string LicensePlate { get; set; }
        public bool IsAvailable { get; set; }

        // Конструктор
        public Driver(string fullName, string carModel, string licensePlate)
        {
            FullName = fullName;
            CarModel = carModel;
            LicensePlate = licensePlate;
            IsAvailable = true; // За замовчуванням вільний
        }

        // Методи
        public void AcceptOrder()
        {
            // Реалізація прийому замовлення водієм
        }

        public void CompleteOrder()
        {
            // Реалізація завершення поїздки
        }
    }
}