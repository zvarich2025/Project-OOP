using System;

namespace TaxiDispatcher
{
    public class TripRoute
    {
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }

        public TripRoute()
        {
            StartPoint = "Центр";
            EndPoint = "Центр";
        }

        public TripRoute(string startPoint, string endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        public TripRoute(TripRoute previousRoute)
        {
            this.StartPoint = previousRoute.StartPoint;
            this.EndPoint = previousRoute.EndPoint;
        }

        // --- ПРЕДИКАТНА ФУНКЦІЯ ---
        // Перевіряє, чи маршрут є валідним (пункт відправлення не дорівнює пункту призначення)
        public bool IsValidRoute()
        {
            return !string.IsNullOrEmpty(StartPoint) &&
                   !string.IsNullOrEmpty(EndPoint) &&
                   StartPoint.Trim().ToLower() != EndPoint.Trim().ToLower();
        }
    }
}