using System;

namespace TaxiDispatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            // Вивід службової інформації про студента та варіант
            Console.WriteLine("ПІБ студента: Зварич Владислав Вячеславович");
            Console.WriteLine("Курс: 1");
            Console.WriteLine("Група: ІПЗ-12");
            Console.WriteLine("Варіант завдання: 44 (Моделювання бізнес-процесів при автоматизації роботи диспетчерської служби таксопарку)");
            Console.WriteLine("Версія: 1");
            Console.WriteLine("--------------------------------------------------");

            // Старт імітації
            Console.WriteLine("Старт імітації");

            // Ініціалізація об'єктів (перевірка, що класи створені та компилюються)
            DispatcherSystem dispatcher = new DispatcherSystem("Smart Taxi Dispatcher");
            Passenger passenger = new Passenger("Олексій", "+380931234567", "вул. Хрещатик, 1");
            Driver driver = new Driver("Петро Коваленко", "Toyota Camry", "AA1234BB");
            Order order = new Order(101, "вул. Хрещатик, 1", "проспект Перемоги, 45", 150.0);

            // Тестовий виклик базового методу (просто для демонстрації зв'язку)
            order.UpdateStatus("Очікування водія");

            // Фініш імітації
            Console.WriteLine("Фініш імітації");

            // Залишаємо вікно консолі відкритим
            Console.ReadLine();
        }
    }
}