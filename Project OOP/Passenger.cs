using System;

namespace TaxiDispatcher
{
    public class Passenger
    {
        // Поля (Властивості)
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string CurrentLocation { get; set; }

        // Конструктор
        public Passenger(string name, string phoneNumber, string currentLocation)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            CurrentLocation = currentLocation;
        }

        // Методи (Заглушки для першої версії)
        public void RequestRide(string destination)
        {
            // Логіка створення запиту буде в наступних версіях
        }
    }
}