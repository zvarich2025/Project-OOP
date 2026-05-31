using System;
using System.Text;

namespace TaxiDispatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            // Вмикаємо UTF-8 для коректного відображення української мови
            Console.OutputEncoding = Encoding.UTF8;

            // 1. ЗАВАНТАЖУЄМО МОВУ З ФАЙЛУ uk.json
            LocalizationManager.LoadLanguage("uk");

            // 2. БЕРЕМО ТЕКСТИ ЗІ СЛОВНИКА І ВІДПРАВЛЯЄМО В LOGGER
            // Зверніть увагу: тепер тут Logger.Log замість Console.WriteLine
            Logger.Log(LocalizationManager.GetString("StudentInfo"));
            Logger.Log(LocalizationManager.GetString("LabHeader"));
            Logger.Log("==================================================\n");

            Logger.Log(LocalizationManager.GetString("SimulationStart"));

            Passenger p1 = new Passenger("Олексій", "+380931112233", 500.0);
            Driver d1 = new Driver("Микола", "+380501234567", "Renault", "AA1111BB");

            Person[] users = { p1, d1 };
            foreach (var user in users)
            {
                // PrintRole всередині вже використовує Logger.Log, тому тут все добре
                user.PrintRole();
            }

            // ... (тут логіка створення замовлень, якщо потрібно) ...

            Logger.Log("\n" + LocalizationManager.GetString("SimulationFinish"));

            // 3. ОБОВ'ЯЗКОВО ЗБЕРІГАЄМО ЛОГ У JSON ФАЙЛ ПЕРЕД ВИХОДОМ
            Logger.SaveToJson("simulation_dictionary.json");

            Console.ReadLine();
        }
    }
}