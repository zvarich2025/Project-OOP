using System;

namespace TaxiDispatcher
{
    public interface ITripManager
    {
        bool ProcessOrder();
        void CompleteTrip();
    }
}