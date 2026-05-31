using System;

namespace TaxiDispatcher
{
    // ПОМИЛКА: Аналогічно забули base()
    public class Driver : Person
    {
        public string CarModel { get; set; }
        public bool IsAvailable { get; set; }

        public Driver(string name, string phoneNumber, string carModel)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            CarModel = carModel;
        }

        public override void PrintRole()
        {
            Console.WriteLine("Я - Водій");
        }
    }
}