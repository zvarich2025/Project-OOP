using System;

namespace TaxiDispatcher
{
    public class Passenger : Person
    {
        public double Balance { get; set; }
        public bool IsVip { get; set; }

        public Passenger(string name, string phoneNumber, double initialBalance) : base(name, phoneNumber)
        {
            Balance = initialBalance;
            IsVip = false;
        }

        public override void PrintRole() => Logger.Log($"[Роль]: Пасажир {Name} (VIP: {IsVip})");

        public bool HasValidContactInfo() => !string.IsNullOrEmpty(PhoneNumber);
        public bool CanAffordRide(double cost) => Balance >= cost;
        public void DeductBalance(double amount) { if (amount > 0 && Balance >= amount) Balance -= amount; }

        public static Passenger operator +(Passenger p, double amount) { p.Balance += amount; return p; }
        public static bool operator true(Passenger p) => p.Balance > 0;
        public static bool operator false(Passenger p) => p.Balance <= 0;
    }
}