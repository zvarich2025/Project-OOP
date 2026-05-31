using System;
using System.Text;

namespace TaxiDispatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            // Вмикаємо UTF-8 для коректного відображення української мови!
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("ПІБ студента: Іванов Іван Іванович");
            Console.WriteLine("Курс: 2 | Група: ІП-41 | Варіант: 44");
            Console.WriteLine("Версія 5 (Успадкування, Абстрактні класи, Інтерфейси)\n");

            // Тест абстрактного класу Person (Створення нащадків)
            Passenger p1 = new Passenger("Олексій", "+380931112233", 500.0);
            Driver d1 = new Driver("Микола", "+380501234567", "Renault", "AA1111BB");
            Driver d2 = new Driver("Степан", "+380671234567", "Skoda", "BC2222CC");

            Console.WriteLine("--- ПЕРЕВІРКА ПОЛІМОРФІЗМУ (Абстрактний метод PrintRole) ---");
            // Можемо покласти їх в масив базового типу Person
            Person[] users = { p1, d1, d2 };
            foreach (var user in users)
            {
                user.PrintRole();
            }

            Console.WriteLine("\n--- ЗАДАЧА 3 ПРІОРИТЕТУ: Автоматична черга на стоянці ---");
            DispatcherSystem dispatcher = new DispatcherSystem("SmartCity Taxi");

            // Водії приїжджають на базу
            dispatcher.AddDriverToStand(d1);
            dispatcher.AddDriverToStand(d2);

            // Клієнт замовляє таксі, беремо першого з черги
            Driver assignedDriver = dispatcher.GetNextAvailableDriver();

            if (assignedDriver != null)
            {
                // Використовуємо інтерфейс ITripManager
                ITripManager order1 = new Order(1, p1, assignedDriver, "Вокзал", "Хрещатик");

                dispatcher.DispatchLog((Order)order1);
                order1.CompleteTrip();
            }

            Console.WriteLine("\n[Система]: Роботу завершено. Натисніть Enter для виходу.");
            Console.ReadLine();
        }
    }
}