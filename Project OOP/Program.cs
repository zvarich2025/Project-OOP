using System;

namespace TaxiDispatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            // Службова інформація (ЗАМІНІТЬ НА СВОЇ ДАНІ)
            Console.WriteLine("ПІБ студента: Іванов Іван Іванович");
            Console.WriteLine("Курс: 2");
            Console.WriteLine("Група: ІП-41");
            Console.WriteLine("Варіант завдання: 44 (Диспетчерська служба таксопарку)");
            Console.WriteLine("Версія: 2 (Конструктори та аксесори класів)");
            Console.WriteLine("--------------------------------------------------\n");

            Console.WriteLine("Старт імітації");
            Console.WriteLine("==================================================");

            // --- ДЕМОНСТРАЦІЯ РОБОТИ КОНСТРУКТОРІВ ТА АКСЕСОРІВ ---

            // 1. Тест статичного конструктора та властивості тільки для читання
            Console.WriteLine($"\n[Тест]: Кількість пасажирів до створення: {Passenger.TotalPassengers}");

            // 2. Тест конструктора з параметрами + Властивості, що автоматично реалізуються
            Passenger passenger1 = new Passenger("Олексій Петренко", "+380501112233");

            // 3. Тест конструктора без параметрів (який викликає інший конструктор через :this)
            Passenger passengerDefault = new Passenger();

            // 4. Тест конструктора копії
            Passenger passengerCopy = new Passenger(passenger1);
            Console.WriteLine($"[Протокол аксесорів]: Ім'я копії пасажира: {passengerCopy.Name}");

            Console.WriteLine($"\n[Тест]: Загальна кількість пасажирів тепер: {Passenger.TotalPassengers}");
            Console.WriteLine("--------------------------------------------------");

            // 5. Тест конструктора водія з перевантаженням (this) та властивостей з валідацією
            Driver driver1 = new Driver("Сергій Коваленко", "Skoda Octavia", "AA5555XX");
            Driver driver2 = new Driver("Андрій Бондар", "AI99999BB"); // Викличе дефолтне авто

            // 6. Тест закритого конструктора через фабричний метод
            Driver systemDriver = Driver.CreateSystemDriver();
            Console.WriteLine($"[Протокол аксесорів]: Закритий конструктор створив: {systemDriver.FullName} ({systemDriver.LicensePlate})");
            Console.WriteLine("--------------------------------------------------");

            // 7. Тест зв'язків АГРЕГАЦІЇ та КОМПОЗИЦІЇ в класі Order
            Console.WriteLine("\nСтворення замовлення (Агрегація водія/пасажира + Композиція маршруту):");
            Order order1 = new Order(101, passenger1, driver1, "вул. Хрещатик, 24", "Аеропорт Бориспіль", 450.0);

            // Вивід значень атрибутів (Протокол аксесорів)
            Console.WriteLine("\n[ПРОТОКОЛ АТРИБУТІВ ЗАМОВЛЕННЯ №1]:");
            Console.WriteLine($"Пасажир: {order1.CurrentPassenger.Name} (Тел: {order1.CurrentPassenger.PhoneNumber})");
            Console.WriteLine($"Водій: {order1.AssignedDriver.FullName} (Авто: {order1.AssignedDriver.CarModel})");
            Console.WriteLine($"Маршрут (Композиція): З [{order1.Route.StartPoint}] до [{order1.Route.EndPoint}]");
            Console.WriteLine($"Вартість: {order1.Price} грн.");

            // 8. Тест конструктора копії для Order (Глибоке копіювання маршруту)
            Order orderRebooked = new Order(order1, 102);
            Console.WriteLine($"\n[ПРОТОКОЛ КОПІЇ ЗАМОВЛЕННЯ №2]:");
            Console.WriteLine($"Нове замовлення: №{orderRebooked.OrderId}, Маршрут: {orderRebooked.Route.StartPoint} -> {orderRebooked.Route.EndPoint}");

            Console.WriteLine("==================================================");
            Console.WriteLine("Фініш імітації");

            Console.ReadLine();
        }
    }
}