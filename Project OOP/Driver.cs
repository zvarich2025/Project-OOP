using System;

namespace TaxiDispatcher
{
    public class Driver : Person
    {
        public string CarModel { get; set; }
        public string LicensePlate { get; }
        public bool IsAvailable { get; set; }
        public int CompletedTrips { get; set; }

        public Driver(string name, string phoneNumber, string carModel, string licensePlate) : base(name, phoneNumber)
        {
            CarModel = carModel;
            LicensePlate = licensePlate;
            IsAvailable = true;
            CompletedTrips = 0;
        }

        // Використовуємо Logger
        public override void PrintRole() => Logger.Log($"[Роль]: Водій {Name} на {CarModel}");

        public bool IsReadyForOrder() => IsAvailable;
        public static Driver operator ++(Driver d) { d.CompletedTrips++; return d; }
        public static Driver operator -(Driver d) { d.IsAvailable = false; return d; }
        public static Driver operator +(Driver d) { d.IsAvailable = true; return d; }
    }
}