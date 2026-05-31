using System;
using System.Collections.Generic; // Потрібно для Queue

namespace TaxiDispatcher
{
    public class DispatcherSystem
    {
        public string Title { get; set; }

        // ЗАДАЧА 3 ПРІОРИТЕТУ: Автоматична черга водіїв на стоянці
        private Queue<Driver> driverStandQueue;

        public DispatcherSystem(string title)
        {
            Title = title;
            driverStandQueue = new Queue<Driver>();
        }

        // Додавання водія в чергу
        public void AddDriverToStand(Driver driver)
        {
            driverStandQueue.Enqueue(driver);
            Console.WriteLine($"[Стоянка]: Водій {driver.Name} заїхав на стоянку. В черзі: {driverStandQueue.Count} авто.");
        }

        // Отримання першого вільного водія
        public Driver GetNextAvailableDriver()
        {
            if (driverStandQueue.Count > 0)
            {
                Driver nextDriver = driverStandQueue.Dequeue();
                Console.WriteLine($"[Стоянка]: Водій {nextDriver.Name} виїхав на замовлення. Залишилось: {driverStandQueue.Count}.");
                return nextDriver;
            }
            Console.WriteLine("[УВАГА]: Немає вільних водіїв на стоянці!");
            return null;
        }

        public void DispatchLog(Order order)
        {
            Console.WriteLine($"[Диспетчер {Title}]: Обробка замовлення №{order.OrderId}...");
            if (order.CurrentPassenger.IsVip) order = order / 1.2;

            bool success = order.ProcessOrder(); // Виклик методу інтерфейсу
            if (success) Console.WriteLine($"[УСПІХ]: Замовлення схвалено! Статус: {order.Status}");
            else Console.WriteLine($"[ВІДХИЛЕНО]: Недостатньо коштів.");
        }
    }
}