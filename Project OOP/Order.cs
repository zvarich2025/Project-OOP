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

        public Order()
        {
            OrderId = 0;
            Price = 0.0;
            Route = new TripRoute();
            Status = "Нове";
        }

        public Order(int orderId, Passenger passenger, Driver driver, string start, string end)
        {
            OrderId = orderId;
            CurrentPassenger = passenger;
            AssignedDriver = driver;
            Route = new TripRoute(start, end);
            Status = "Нове";
            CalculateOrderPrice(); // Автоматичний розрахунок при створенні
        }

        // --- МЕТОД РОЗРАХУНКУ ВАРТОСТІ ---
        private void CalculateOrderPrice()
        {
            // Імітація розрахунку: базова ставка 50 грн + довжина назв вулиць як сурогат відстані
            double distanceFactor = (Route.StartPoint.Length + Route.EndPoint.Length) * 5;
            Price = 50.0 + distanceFactor;
        }

        // --- МЕТОД КЕРУВАННЯ ЗАМОВЛЕННЯМ ---
        public bool ProcessOrder()
        {
            // Комплексна перевірка умов за допомогою предикатів інших класів
            if (!Route.IsValidRoute())
            {
                Status = "Відхилено: Невалідний маршрут";
                return false;
            }
            if (!CurrentPassenger.HasValidContactInfo())
            {
                Status = "Відхилено: Невірний телефон клієнта";
                return false;
            }
            if (!AssignedDriver.IsReadyForOrder())
            {
                Status = "Відхилено: Водій зайнятий";
                return false;
            }
            if (!CurrentPassenger.CanAffordRide(Price))
            {
                Status = "Відхилено: Недостатньо коштів";
                return false;
            }

            // Якщо всі предикати повернули true — успішно оформлюємо
            CurrentPassenger.DeductBalance(Price);
            AssignedDriver.ToggleAvailability(false); // Водій стає зайнятим
            Status = "Виконується";
            return true;
        }

        // --- ПРЕДИКАТНА ФУНКЦІЯ ---
        // Перевіряє, чи замовлення зараз знаходиться в активному стані поїздки
        public bool IsActiveTrip()
        {
            return Status == "Виконується";
        }

        public void CompleteTrip()
        {
            if (Status == "Виконується")
            {
                Status = "Завершено";
                AssignedDriver.ToggleAvailability(true); // Водій знову вільний
            }
        }
    }
}