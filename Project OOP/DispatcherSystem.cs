using System;

namespace TaxiDispatcher
{
    public class DispatcherSystem
    {
        public string Title { get; set; }
        public bool IsActive { get; set; }

        public DispatcherSystem() : this("Єдина Диспетчерська") { }

        public DispatcherSystem(string title)
        {
            Title = title;
            IsActive = true;
        }

        // Метод координації бізнес-процесу
        public void DispatchLog(Order order)
        {
            Console.WriteLine($"[Диспетчер {Title}]: Обробка замовлення №{order.OrderId}...");
            bool success = order.ProcessOrder();

            if (success)
            {
                Console.WriteLine($"[УСПІХ]: Замовлення схвалено! Вартість: {order.Price} грн. Статус: {order.Status}");
            }
            else
            {
                Console.WriteLine($"[ВІДХИЛЕНО]: Не вдалося виконати. Причина: {order.Status}");
            }
        }
    }
}