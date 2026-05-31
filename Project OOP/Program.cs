using System;

namespace TaxiDispatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            // Службова інформація
            Console.WriteLine("ПІБ студента: Зварич Владислав Вячеславович");
            Console.WriteLine("Курс: 1");
            Console.WriteLine("Група: ІПЗ-12");
            Console.WriteLine("Варіант завдання: 44 (Диспетчерська служба таксопарку)");
            Console.WriteLine("Версія: 3 (Реалізація предикатних функцій та методів)");
            Console.WriteLine("--------------------------------------------------\n");

            Console.WriteLine("Старт імітації");
            Console.WriteLine("==================================================");

            // 1. Ініціалізація системи та сутностей
            DispatcherSystem centralDispatcher = new DispatcherSystem("SmartCity Taxi");

            Passenger passengerGood = new Passenger("Олексій Новіков", "+380931112233", 500.0);
            Passenger passengerPoor = new Passenger("Бідний Студент", "+380507778899", 20.0); // Мало грошей

            Driver driverStandard = new Driver("Микола Грицай", "Renault Logan", "AA7711BB");

            // 2. Виклик предикатів для перевірки стану перед бізнес-процесом
            Console.WriteLine("\n--- ПЕРЕВІРКА СТАНІВ ОБ'ЄКТІВ (ПРЕДИКАТИ) ---");
            Console.WriteLine($"Чи валідні контакти пасажира 1? {passengerGood.HasValidContactInfo()}");
            Console.WriteLine($"Чи готовий водій Микола до роботи? {driverStandard.IsReadyForOrder()}");
            Console.WriteLine($"Чи вистачить грошей бідному пасажиру на поїздку за 150 грн? {passengerPoor.CanAffordRide(150.0)}");
            Console.WriteLine("--------------------------------------------------");

            // 3. СЦЕНАРІЙ №1: Спроба замовлення з недостатнім балансом
            Console.WriteLine("\n[СЦЕНАРІЙ 1]: Замовлення від пасажира з малим балансом");
            Order orderFail = new Order(1, passengerPoor, driverStandard, "вул. Політехнічна", "вул. Хрещатик");

            centralDispatcher.DispatchLog(orderFail);
            Console.WriteLine($"Поточний статус замовлення №1 в системі: {orderFail.Status}");
            Console.WriteLine($"Предикат активності поїздки: {orderFail.IsActiveTrip()}");

            // 4. СЦЕНАРІЙ №2: Успішне замовлення
            Console.WriteLine("\n[СЦЕНАРІЙ 2]: Замовлення від платоспроможного пасажира");
            Order orderSuccess = new Order(2, passengerGood, driverStandard, "вул. Політехнічна", "Аеропорт");

            // Перевіримо предикат валідності створеного маршруту всередині замовлення
            Console.WriteLine($"Внутрішній предикат: чи коректний маршрут замовлення? {orderSuccess.Route.IsValidRoute()}");

            // Диспетчер обробляє замовлення
            centralDispatcher.DispatchLog(orderSuccess);
            Console.WriteLine($"Предикат активності поїздки тепер: {orderSuccess.IsActiveTrip()}");
            Console.WriteLine($"Залишок балансу пасажира: {passengerGood.Balance} грн.");
            Console.WriteLine($"Чи вільний водій Микола тепер? {driverStandard.IsAvailable}");

            // 5. Завершення успішної поїздки
            Console.WriteLine("\n--- ЗАВЕРШЕННЯ ПОЇЗДКИ ---");
            orderSuccess.CompleteTrip();
            Console.WriteLine($"Статус замовлення №2 після фінішу: {orderSuccess.Status}");
            Console.WriteLine($"Чи вільний водій Микола знову? {driverStandard.IsAvailable}");

            Console.WriteLine("==================================================");
            Console.WriteLine("Фініш імітації");

            Console.ReadLine();
        }
    }
}