using System;

namespace TaxiDispatcher
{
    public class Order
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

        private void CalculateOrderPrice()
        {
            double distanceFactor = (Route.StartPoint.Length + Route.EndPoint.Length) * 5;
            Price = 50.0 + distanceFactor;
        }

        public bool ProcessOrder()
        {
            if (!Route.IsValidRoute() || !CurrentPassenger.HasValidContactInfo() || !AssignedDriver.IsReadyForOrder() || !CurrentPassenger.CanAffordRide(Price))
            {
                Status = "Відхилено";
                return false;
            }

            CurrentPassenger.DeductBalance(Price);
            AssignedDriver = -AssignedDriver; // Використовуємо унарний мінус (робимо зайнятим)
            Status = "Виконується";
            return true;
        }

        public void CompleteTrip()
        {
            if (Status == "Виконується")
            {
                Status = "Завершено";
                AssignedDriver = +AssignedDriver; // Використовуємо унарний плюс (знову вільний)
                AssignedDriver++; // Збільшуємо лічильник поїздок через перевантажений ++
            }
        }

        // --- ПЕРЕВАНТАЖЕННЯ ОПЕРАТОРІВ ---

        // 1. Бінарні * та / (Застосування коефіцієнтів, наприклад, "гарячий час" або знижка)
        public static Order operator *(Order o, double multiplier)
        {
            o.Price *= multiplier;
            return o;
        }

        public static Order operator /(Order o, double divider)
        {
            if (divider != 0) o.Price /= divider;
            return o;
        }

        // 2. Оператори порівняння (Порівняння замовлень за вартістю)
        public static bool operator >(Order o1, Order o2) => o1.Price > o2.Price;
        public static bool operator <(Order o1, Order o2) => o1.Price < o2.Price;
        public static bool operator >=(Order o1, Order o2) => o1.Price >= o2.Price;
        public static bool operator <=(Order o1, Order o2) => o1.Price <= o2.Price;
    }
}