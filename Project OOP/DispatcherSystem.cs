using System;

namespace TaxiDispatcher
{
    public class DispatcherSystem
    {
        public string Title { get; set; }

        // Властивість читання-запису
        private bool isActive;
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        // 1. Конструктор без параметрів
        public DispatcherSystem() : this("Єдина Диспетчерська") // 6. Виклик іншого конструктора
        {
        }

        // 2. Конструктор з параметрами
        public DispatcherSystem(string title)
        {
            Title = title;
            IsActive = true;
            Console.WriteLine($"[Конструктор з параметрами]: Запущено систему '{Title}'");
        }
    }
}