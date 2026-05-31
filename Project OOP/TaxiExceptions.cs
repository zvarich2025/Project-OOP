using System;

namespace TaxiDispatcher
{
    public class InsufficientFundsException : Exception
    {
        public double RequiredAmount { get; }
        public InsufficientFundsException(string message, double requiredAmount) : base(message)
        {
            RequiredAmount = requiredAmount;
        }
    }

    public class DriverUnavailableException : Exception
    {
        public DriverUnavailableException(string message) : base(message) { }
    }

    public class InvalidRouteException : Exception
    {
        public InvalidRouteException(string message) : base(message) { }
    }
}