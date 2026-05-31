using System;
using System.Collections.Generic;
using System.Linq;

namespace TaxiDispatcher
{
    public class DispatcherSystem
    {
        public string Title { get; set; }
        private Queue<Driver> driverStandQueue;
        private List<DispatcherEmployee> brigade;

        // --- БАЗА ПОРУШНИКІВ ---
        private List<string> blacklistedPassengerPhones;
        private List<string> blacklistedPassengerNames;
        private List<string> blacklistedDriverPlates;

        private int enterpriseOrders = 0;
        private int mobileOrders = 0;

        public DispatcherSystem(string title)
        {
            Title = title;
            driverStandQueue = new Queue<Driver>();
            brigade = new List<DispatcherEmployee>();

            // 1. Чорний список телефонів клієнтів
            blacklistedPassengerPhones = new List<string> { "+380000000000", "+380111111111" };

            // 2. Чорний список імен клієнтів
            blacklistedPassengerNames = new List<string> { "Хуліган", "Шахрай", "Бандит" };

            // 3. Чорний список водіїв-порушників (за номерами авто)
            blacklistedDriverPlates = new List<string> { "AA1111BB" };
        }

        public void AddDispatcher(DispatcherEmployee emp) => brigade.Add(emp);

        public void AddDriverToStand(Driver driver)
        {
            driver.State = DriverState.OnStand;
            driverStandQueue.Enqueue(driver);
            Logger.Log($"[Стоянка]: Водій {driver.Name} ({driver.CarClass}) заїхав на базу.");
        }

        public Driver GetNextAvailableDriver(TaxiClass desiredClass)
        {
            Driver foundDriver = driverStandQueue.FirstOrDefault(d =>
                d.CarClass == desiredClass &&
                d.State == DriverState.OnStand &&
                !blacklistedDriverPlates.Contains(d.LicensePlate));

            if (foundDriver != null)
            {
                Logger.Log($"[Стоянка]: Знайдено авто! Водій {foundDriver.Name} виїхав на замовлення.");
                return foundDriver;
            }

            throw new DriverUnavailableException($"На жаль, зараз немає вільних авто класу {desiredClass} (або водії заблоковані службою безпеки)!");
        }

        public void DispatchLog(Order order)
        {
            // --- ПЕРЕВІРКА СЛУЖБИ БЕЗПЕКИ ---
            bool isBlacklistedPhone = blacklistedPassengerPhones.Contains(order.CurrentPassenger.PhoneNumber);

            // Перевіряємо ім'я (StringComparison.OrdinalIgnoreCase означає, що регістр літер не має значення)
            bool isBlacklistedName = blacklistedPassengerNames.Any(n =>
                n.Equals(order.CurrentPassenger.Name, StringComparison.OrdinalIgnoreCase));

            if (isBlacklistedPhone || isBlacklistedName)
            {
                Logger.Log($"\n[СЛУЖБА БЕЗПЕКИ]: УВАГА! Пасажир {order.CurrentPassenger.Name} (Тел: {order.CurrentPassenger.PhoneNumber}) знаходиться у ЧОРНОМУ СПИСКУ!");
                Logger.Log("[СЛУЖБА БЕЗПЕКИ]: У прийнятті замовлення ВІДМОВЛЕНО. Екіпаж не виїжджає.");
                return; // Миттєво припиняємо обробку замовлення
            }
            // ---------------------------------

            if (order.Source == OrderSource.Enterprise) enterpriseOrders++;
            else mobileOrders++;

            var receiver = brigade.FirstOrDefault(b => b.Role == DispatcherRole.OrderReceiver) ?? brigade[0];
            var manager = brigade.FirstOrDefault(b => b.Role == DispatcherRole.FleetManager) ?? brigade[0];

            receiver.HandledOrders++;
            Logger.Log($"[Бригада]: Диспетчер {receiver.Name} прийняв замовлення №{order.OrderId}.");

            if (!string.IsNullOrEmpty(order.PassengerComment))
            {
                Logger.Log($"[Коментар від клієнта]: \"{order.PassengerComment}\"");
            }

            if (order.CurrentPassenger.IsVip) order = order / 1.2;

            if (order.ProcessOrder())
            {
                Logger.Log($"[Бригада]: Логіст {manager.Name} контролює виконання.");
                order.PassengerBoarding();
                order.CompleteTrip();

                AddDriverToStand(order.AssignedDriver);
            }
        }

        public void GenerateShiftReport()
        {
            Logger.Log("\n================ ЗВІТ ПО ЗМІНІ ================");
            Logger.Log($"Усього замовлень з мобільних: {mobileOrders}");
            Logger.Log($"Усього замовлень з підприємств: {enterpriseOrders}");

            Logger.Log("\n--- Робота водіїв ---");
            foreach (var driver in driverStandQueue.Distinct())
            {
                Logger.Log($"- Водій {driver.Name} ({driver.CarClass}): Поїздок: {driver.CompletedTrips}. Стан: {driver.State}");
            }
            Logger.Log("===============================================\n");
        }
    }
}