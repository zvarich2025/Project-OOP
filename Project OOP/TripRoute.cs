using System;

namespace TaxiDispatcher
{
    public class TripRoute
    {
        // Властивість читання та запису (Read-Write Property)
        private string startPoint;
        public string StartPoint
        {
            get { return startPoint; }
            set { startPoint = !string.IsNullOrEmpty(value) ? value : "Невідомо"; }
        }

        // Властивість, що автоматично реалізується (Auto-implemented Property)
        public string EndPoint { get; set; }

        // 1. Конструктор без параметрів (Default constructor)
        public TripRoute()
        {
            StartPoint = "Центр";
            EndPoint = "Центр";
        }

        // 2. Конструктор з параметрами (Initialization constructor)
        public TripRoute(string startPoint, string endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        // 5. Конструктор копії (Copy constructor)
        public TripRoute(TripRoute previousRoute)
        {
            this.StartPoint = previousRoute.StartPoint;
            this.EndPoint = previousRoute.EndPoint;
        }
    }
}