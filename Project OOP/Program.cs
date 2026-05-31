using System;
using System.Text;

namespace TaxiDispatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Logger.Log("ПІБ студента: Зварич Владислав В'ячеславович");
            Logger.Log("Навчальний заклад: КНУ ім. Тараса Шевченка | Група: ІПЗ-12");
            Logger.Log("Варіант: 44 | Версія 5 (Абстракції + Вивід у JSON)\n");

            Passenger p1 = new Passenger("Олексій", "+380931112233", 500.0);
            Driver d1 = new Driver("Микола", "+380501234567", "Renault", "AA1111BB");
            Driver d2 = new Driver("Степан", "+380671234567", "Skoda", "BC2222CC");

            Logger.Log("--- ПЕРЕВІРКА ПОЛІМОРФІЗМУ ---");
            Person[] users = { p1, d1, d2 };
            foreach (var user in users)
            {
                user.PrintRole();
            }

            Logger.Log("\n--- АВТОМАТИЧНА ЧЕРГА НА СТОЯНЦІ ---");
            DispatcherSystem dispatcher = new DispatcherSystem("SmartCity Taxi");

            dispatcher.AddDriverToStand(d1);
            dispatcher.AddDriverToStand(d2);

            Driver assignedDriver = dispatcher.GetNextAvailableDriver();

            if (assignedDriver != null)
            {
                ITripManager order1 = new Order(1, p1, assignedDriver, "Вокзал", "Хрещатик");

                dispatcher.DispatchLog((Order)order1);
                order1.CompleteTrip();
            }

            // ФІНАЛЬНИЙ АКОРД: Зберігаємо всю історію в JSON
            Logger.SaveToJson("simulation_result.json");

            Console.ReadLine();
        }
    }
}