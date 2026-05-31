using System;
using System.Collections.Generic;

namespace TaxiDispatcher
{
    // Джерело замовлення (Мобільний, Підприємство, Вулиця)
    public enum OrderSource { Mobile, Enterprise, AddressLookup }

    // Стан водія
    public enum DriverState { OnStand, Dispatching, WithPassenger, OnLunch, OutOfTown }

    // Роль диспетчера в бригаді
    public enum DispatcherRole { OrderReceiver, FleetManager, Universal }

    // Імітація бази даних міста (оновлення через Internet)
    public static class CityDatabase
    {
        private static List<string> validStreets = new List<string> { "Вокзал", "Аеропорт", "Хрещатик", "Поділ", "Оболонь" };

        public static void UpdateFromInternet()
        {
            Logger.Log("[База Міста]: Підключення до Internet... Оновлення бази адрес.");
            validStreets.Add("Новий ЖК 'СмартСіті'");
        }

        public static bool IsValidAddress(string address)
        {
            return validStreets.Contains(address);
        }
    }
}