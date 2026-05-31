using System;

namespace TaxiDispatcher
{
    // ПОМИЛКА: Немає виклику : base(name, phoneNumber)
    public class Passenger : Person
    {
        public double Balance { get; set; }
        public bool IsVip { get; set; }

        public Passenger(string name, string phoneNumber, double initialBalance)
        {
            Name = name; // Це невірно в ООП, треба передавати в base
            PhoneNumber = phoneNumber;
            Balance = initialBalance;
        }

        public override void PrintRole()
        {
            Console.WriteLine("Я - Пасажир");
        }
    }
}