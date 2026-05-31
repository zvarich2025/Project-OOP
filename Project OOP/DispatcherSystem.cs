using System;

namespace TaxiDispatcher
{
    public class DispatcherSystem
    {
        public string Title { get; set; }

        public DispatcherSystem(string title) { Title = title; }

        // ЗАДАЧА 2 ПРІОРИТЕТУ: Робота з VIP клієнтами
        public void MakeVip(Passenger passenger)
        {
            passenger.IsVip = true;
            Console.WriteLine($"[Диспетчерська]: Пасажир {passenger.Name} отримав статус VIP!");
        }

        public void DispatchLog(Order order)
        {
            Console.WriteLine($"[Диспетчер {Title}]: Обробка замовлення №{order.OrderId}...");

            // Якщо клієнт VIP - робимо знижку 20% (використовуємо перевантажений оператор /)
            if (order.CurrentPassenger.IsVip)
            {
                order = order / 1.2;
                Console.WriteLine($"Застосовано VIP знижку! Нова ціна: {Math.Round(order.Price, 2)} грн.");
            }

            bool success = order.ProcessOrder();
            if (success) Console.WriteLine($"[УСПІХ]: Замовлення схвалено! Статус: {order.Status}");
            else Console.WriteLine($"[ВІДХИЛЕНО]: Не вдалося виконати.");
        }
    }
}