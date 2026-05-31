using System;
using System.Collections.Generic;

namespace TaxiDispatcher
{
    public class DispatcherSystem
    {
        public string Title { get; set; }
        private Queue<Driver> driverStandQueue;

        public DispatcherSystem(string title)
        {
            Title = title;
            driverStandQueue = new Queue<Driver>();
        }

        public void AddDriverToStand(Driver driver)
        {
            driverStandQueue.Enqueue(driver);
            Logger.Log($"[Стоянка]: Водій {driver.Name} заїхав на стоянку. В черзі: {driverStandQueue.Count} авто.");
        }

        public Driver GetNextAvailableDriver()
        {
            if (driverStandQueue.Count > 0)
            {
                Driver nextDriver = driverStandQueue.Dequeue();
                Logger.Log($"[Стоянка]: Водій {nextDriver.Name} виїхав на замовлення. Залишилось в черзі: {driverStandQueue.Count}.");
                return nextDriver;
            }
            Logger.Log("[УВАГА]: Немає вільних водіїв на стоянці!");
            return null;
        }

        public void DispatchLog(Order order)
        {
            Logger.Log($"[Диспетчер {Title}]: Обробка замовлення №{order.OrderId}...");
            if (order.CurrentPassenger.IsVip) order = order / 1.2;

            bool success = order.ProcessOrder();
            if (success) Logger.Log($"[УСПІХ]: Замовлення схвалено! Статус: {order.Status}, Ціна: {Math.Round(order.Price, 2)} грн.");
            else Logger.Log($"[ВІДХИЛЕНО]: Недостатньо коштів.");
        }
    }
}