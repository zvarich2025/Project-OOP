using System;

namespace TaxiDispatcher
{
    public class Driver : Person
    {
        public string CarModel { get; set; }
        public string LicensePlate { get; }
        public TaxiClass CarClass { get; set; }
        public DriverState State { get; set; } // Стан (обід, з клієнтом тощо)
        public DateTime ShiftStartTime { get; private set; } // Початок зміни
        public int CompletedTrips { get; set; }

        public Driver(string name, string phoneNumber, string carModel, string licensePlate, TaxiClass carClass)
            : base(name, phoneNumber)
        {
            CarModel = carModel;
            LicensePlate = licensePlate;
            CarClass = carClass;
            State = DriverState.OnStand;
            ShiftStartTime = DateTime.Now.AddHours(-2); // Ніби водій працює вже 2 години
            CompletedTrips = 0;
        }

        public TimeSpan GetShiftDuration() => DateTime.Now - ShiftStartTime;

        public override void PrintRole() => Logger.Log($"[Роль]: Водій {Name} на {CarModel} (Клас: {CarClass})");

        public bool IsReadyForOrder() => State == DriverState.OnStand;
        public static Driver operator ++(Driver d) { d.CompletedTrips++; return d; }
    }
}