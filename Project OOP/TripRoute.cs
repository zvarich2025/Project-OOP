using System;

namespace TaxiDispatcher
{
    public class TripRoute
    {
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }

        public TripRoute() { StartPoint = "Центр"; EndPoint = "Центр"; }
        public TripRoute(string startPoint, string endPoint) { StartPoint = startPoint; EndPoint = endPoint; }
        public TripRoute(TripRoute previousRoute) { StartPoint = previousRoute.StartPoint; EndPoint = previousRoute.EndPoint; }

        public bool IsValidRoute() => !string.IsNullOrEmpty(StartPoint) && !string.IsNullOrEmpty(EndPoint) && StartPoint.Trim().ToLower() != EndPoint.Trim().ToLower();
    }
}