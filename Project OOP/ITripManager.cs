using System;

namespace TaxiDispatcher
{
    // Інтерфейс для керування поїздкою
    public interface ITripManager
    {
        bool ProcessOrder();
        void CompleteTrip();
    }
}