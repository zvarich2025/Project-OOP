using System;

namespace TaxiDispatcher
{
    // Реалізація інтерфейсу ITripManager
    public class Order : ITripManager
    {
        public Passenger CurrentPassenger { get; set; }
        public Driver AssignedDriver { get; set; }
        public TripRoute Route { get; private set; }
        public int OrderId { get; set; }
        public double Price { get; private set; }
        public string Status { get; private set; }

        public Order(int orderId, Passenger passenger, Driver driver, string start, string end)
        {
            OrderId = orderId;
            CurrentPassenger = passenger;
            AssignedDriver = driver;
            Route = new TripRoute(start, end);
            Status = "Нове";
            CalculateOrderPrice();
        }

        private void CalculateOrderPrice() => Price = 50.0 + ((Route.StartPoint.Length + Route.EndPoint.Length) * 5);

        // Метод з інтерфейсу
        public bool ProcessOrder()
        {
            if (!CurrentPassenger.CanAffordRide(Price)) return false;

            CurrentPassenger.DeductBalance(Price);
            AssignedDriver = -AssignedDriver; // Робимо зайнятим
            Status = "Виконується";
            return true;
        }

        // Метод з інтерфейсу
        public void CompleteTrip()
        {
            if (Status == "Виконується")
            {
                Status = "Завершено";
                AssignedDriver = +AssignedDriver; // Вільний
                AssignedDriver++; // +1 поїздка
            }
        }

        public static Order operator /(Order o, double divider) { if (divider != 0) o.Price /= divider; return o; }
    }
}