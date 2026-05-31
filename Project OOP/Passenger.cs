using System;

namespace TaxiDispatcher
{
    public class Passenger
    {
        private static int passengerCount;
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public double Balance { get; set; } // Додано поле балансу для бізнес-сценарію

        static Passenger()
        {
            passengerCount = 0;
        }

        public Passenger() : this("Анонім", "+380000000000", 0.0) { }

        public Passenger(string name, string phoneNumber) : this(name, phoneNumber, 200.0) { }

        // Розширений конструктор
        public Passenger(string name, string phoneNumber, double initialBalance)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Balance = initialBalance;
            passengerCount++;
        }

        public Passenger(Passenger other)
        {
            this.Name = other.Name;
            this.PhoneNumber = other.PhoneNumber;
            this.Balance = other.Balance;
            passengerCount++;
        }

        public static int TotalPassengers => passengerCount;

        // --- ПРЕДИКАТНА ФУНКЦІЯ ---
        // Перевіряє, чи правильно заповнений номер телефону (спрощена валідація на довжину)
        public bool HasValidContactInfo()
        {
            return !string.IsNullOrEmpty(PhoneNumber) && PhoneNumber.StartsWith("+380") && PhoneNumber.Length == 13;
        }

        // --- ПРЕДИКАТНА ФУНКЦІЯ ---
        // Перевіряє, чи достатньо у пасажира коштів для поїздки
        public bool CanAffordRide(double cost)
        {
            return Balance >= cost;
        }

        // Метод зміни стану
        public void DeductBalance(double amount)
        {
            if (amount > 0 && Balance >= amount)
            {
                Balance -= amount;
            }
        }
    }
}