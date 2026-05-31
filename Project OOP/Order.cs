using System;

namespace TaxiDispatcher
{
    public class Order
    {
        // Реалізація АГРЕГАЦІЇ: замовлення містить посилання на сторонні об'єкти
        public Passenger CurrentPassenger { get; set; }
        public Driver AssignedDriver { get; set; }

        // Реалізація КОМПОЗИЦІЇ: об'єкт маршруту створюється виключно всередині замовлення
        public TripRoute Route { get; private set; }

        public int OrderId { get; set; }
        public double Price { get; set; }

        // 1. Конструктор без параметрів
        public Order()
        {
            OrderId = 0;
            Price = 0.0;
            Route = new TripRoute(); // Композиція за замовчуванням
            Console.WriteLine("[Конструктор без параметрів]: Створено порожнє замовлення.");
        }

        // 2. Конструктор з параметрами (Демонструє Агрегацію та Композицію)
        public Order(int orderId, Passenger passenger, Driver driver, string start, string end, double price)
        {
            OrderId = orderId;
            Price = price;

            // Агрегація (приймаємо готові об'єкти ззовні)
            CurrentPassenger = passenger;
            AssignedDriver = driver;

            // Композиція (об'єкт маршруту створюється ТУТ і належить замовленню)
            Route = new TripRoute(start, end);

            Console.WriteLine($"[Конструктор з параметрами]: Створено Замовлення №{OrderId} (Агрегація + Композиція)");
        }

        // 5. Конструктор копії (Глибоке копіювання композиції)
        public Order(Order previousOrder, int newId)
        {
            this.OrderId = newId;
            this.Price = previousOrder.Price;
            this.CurrentPassenger = previousOrder.CurrentPassenger; // Копія посилання (агрегація)
            this.AssignedDriver = previousOrder.AssignedDriver;

            // Глибоке копіювання композиції (створюємо новий маршрут на основі старого)
            this.Route = new TripRoute(previousOrder.Route);
            Console.WriteLine($"[Конструктор копії]: Переоформлено замовлення №{previousOrder.OrderId} як нове №{newId}");
        }
    }
}