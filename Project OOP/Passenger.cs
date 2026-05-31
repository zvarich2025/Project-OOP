using System;

namespace TaxiDispatcher
{
    public class Passenger
    {
        private static int passengerCount = 0;
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public double Balance { get; set; }
        public bool IsVip { get; set; } // Задача 2 пріоритету (VIP пасажири)

        public Passenger(string name, string phoneNumber, double initialBalance)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Balance = initialBalance;
            IsVip = false;
            passengerCount++;
        }

        public bool HasValidContactInfo() => !string.IsNullOrEmpty(PhoneNumber) && PhoneNumber.StartsWith("+380") && PhoneNumber.Length == 13;
        public bool CanAffordRide(double cost) => Balance >= cost;
        public void DeductBalance(double amount) { if (amount > 0 && Balance >= amount) Balance -= amount; }

        // --- ПЕРЕВАНТАЖЕННЯ ОПЕРАТОРІВ ---

        // 1. Бінарні + та - (Поповнення та зняття коштів з балансу)
        public static Passenger operator +(Passenger p, double amount)
        {
            p.Balance += amount;
            return p;
        }

        public static Passenger operator -(Passenger p, double amount)
        {
            p.Balance -= amount;
            if (p.Balance < 0) p.Balance = 0;
            return p;
        }

        // 2. Унарний ! (Перевірка, чи заблокований/невалідний клієнт)
        public static bool operator !(Passenger p)
        {
            return !p.HasValidContactInfo();
        }

        // 3. Оператори true / false (Чи платоспроможний клієнт загалом)
        public static bool operator true(Passenger p) => p.Balance > 0;
        public static bool operator false(Passenger p) => p.Balance <= 0;
    }
}