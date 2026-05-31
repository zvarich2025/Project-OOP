using System;

namespace TaxiDispatcher
{
    public class DispatcherSystem
    {
        // Поля (Властивості) - у майбутньому тут будуть списки List<Driver> тощо
        public string SystemName { get; set; }

        // Конструктор
        public DispatcherSystem(string systemName)
        {
            SystemName = systemName;
        }

        // Методи
        public void AssignDriverToOrder()
        {
            // Логіка автоматичного пошуку найближчого водія
        }

        public void CalculatePrice()
        {
            // Логіка розрахунку вартості за кілометраж
        }
    }
}