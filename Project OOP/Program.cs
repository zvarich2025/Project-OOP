using System;

namespace TaxiDispatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ПІБ студента: Іванов Іван Іванович");
            Console.WriteLine("Курс: 2 | Група: ІП-41 | Варіант: 44");
            Console.WriteLine("Версія 4 (Перевантаження операторів та задачі 2 пріоритету)\n");

            DispatcherSystem dispatcher = new DispatcherSystem("SmartCity Taxi");
            Passenger p1 = new Passenger("Олексій", "+380931112233", 100.0);
            Driver d1 = new Driver("Микола", "Renault", "AA1111BB");
            Driver d2 = new Driver("Микола Клон", "Daewoo", "AA1111BB"); // Той самий номер

            // 1. Демонстрація перевантаження унарних та true/false для Пасажира
            Console.WriteLine("--- ТЕСТ ОПЕРАТОРІВ ПАСАЖИРА ---");
            Console.WriteLine($"Баланс до: {p1.Balance}");
            p1 = p1 + 500.0; // Бінарний +
            Console.WriteLine($"Баланс після (+ 500): {p1.Balance}");

            if (p1) Console.WriteLine("Оператор true: Пасажир має позитивний баланс.");
            if (!p1) Console.WriteLine("Оператор !: Контакти пасажира НЕ валідні.");
            else Console.WriteLine("Оператор !: Контакти пасажира валідні.");

            // 2. Демонстрація операторів Порівняння (==) для Водія
            Console.WriteLine("\n--- ТЕСТ ОПЕРАТОРІВ ВОДІЯ ---");
            if (d1 == d2) Console.WriteLine("Оператор ==: Це один і той самий водій (збігаються номери).");

            // 3. Задачі 2 пріоритету (VIP та Облік)
            Console.WriteLine("\n--- ТЕСТ ЗАДАЧ 2 ПРІОРИТЕТУ ТА ЗАМОВЛЕНЬ ---");
            dispatcher.MakeVip(p1); // Другий пріоритет: VIP статус

            Order order1 = new Order(1, p1, d1, "Вокзал", "Центр");
            Order order2 = new Order(2, p1, d1, "Вокзал", "Далеке передмістя");

            // Порівняння замовлень (<, >)
            if (order2 > order1) Console.WriteLine($"Оператор >: Замовлення №2 дорожче за №1 ({order2.Price} > {order1.Price})");

            // 4. Демонстрація життєвого циклу з перевантаженнями (*, ++, +d, -d)
            Console.WriteLine("\nПочинається дощ, збільшуємо ціну в 1.5 рази (Оператор *)...");
            order1 = order1 * 1.5;

            dispatcher.DispatchLog(order1);

            Console.WriteLine($"Статус водія після прийняття (працював унарний -d): Вільний = {d1.IsAvailable}");

            order1.CompleteTrip();
            Console.WriteLine($"Поїздку завершено. Кількість поїздок водія (Оператор ++): {d1.CompletedTrips}");
            Console.WriteLine($"Статус водія (працював унарний +d): Вільний = {d1.IsAvailable}");

            Console.ReadLine();
        }
    }
}