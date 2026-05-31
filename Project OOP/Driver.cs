using System;

namespace TaxiDispatcher
{
    public class Driver
    {
        public string FullName { get; set; }
        public string CarModel { get; set; }
        public string LicensePlate { get; }
        public bool IsAvailable { get; set; }
        public int CompletedTrips { get; set; } // Задача 2 пріоритету (Облік поїздок)

        public Driver(string fullName, string carModel, string licensePlate)
        {
            FullName = fullName;
            CarModel = carModel;
            LicensePlate = licensePlate;
            IsAvailable = true;
            CompletedTrips = 0;
        }

        public bool IsReadyForOrder() => IsAvailable && !string.IsNullOrEmpty(FullName);
        public void ToggleAvailability(bool status) => IsAvailable = status;

        // --- ПЕРЕВАНТАЖЕННЯ ОПЕРАТОРІВ ---

        // 1. Унарні ++ та -- (Збільшення/зменшення кількості виконаних поїздок)
        public static Driver operator ++(Driver d)
        {
            d.CompletedTrips++;
            return d;
        }

        public static Driver operator --(Driver d)
        {
            if (d.CompletedTrips > 0) d.CompletedTrips--;
            return d;
        }

        // 2. Унарні + та - (Швидке перемикання статусу: + вийшов на зміну, - пішов зі зміни)
        public static Driver operator +(Driver d)
        {
            d.IsAvailable = true;
            return d;
        }

        public static Driver operator -(Driver d)
        {
            d.IsAvailable = false;
            return d;
        }

        // 3. Оператори порівняння == та != (Перевірка, чи це один і той самий водій за номерами авто)
        public static bool operator ==(Driver d1, Driver d2)
        {
            if (ReferenceEquals(d1, d2)) return true;
            if (d1 is null || d2 is null) return false;
            return d1.LicensePlate == d2.LicensePlate;
        }

        public static bool operator !=(Driver d1, Driver d2) => !(d1 == d2);

        // Системні перевизначення для коректної роботи ==
        public override bool Equals(object obj) => obj is Driver driver && this == driver;
        public override int GetHashCode() => LicensePlate?.GetHashCode() ?? 0;
    }
}