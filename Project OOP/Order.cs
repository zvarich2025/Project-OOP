using System;

namespace TaxiDispatcher
{
    public class Order : ITripManager
    {
        public Passenger CurrentPassenger { get; set; }
        public Driver AssignedDriver { get; set; }
        public TripRoute Route { get; private set; }
        public int OrderId { get; set; }
        public double Price { get; private set; }
        public string Status { get; private set; }
        public OrderSource Source { get; set; }

        // НОВА ВЛАСТИВІСТЬ: Коментар для водія
        public string PassengerComment { get; set; }

        public Order(int orderId, Passenger passenger, Driver driver, string start, string end, OrderSource source, string comment = "")
        {
            OrderId = orderId;
            CurrentPassenger = passenger;
            AssignedDriver = driver;
            Route = new TripRoute(start, end);
            Source = source;
            PassengerComment = comment;
            Status = "Прийнято";
            CalculateOrderPrice();
        }

        private void CalculateOrderPrice() => Price = 50.0 + ((Route.StartPoint.Length + Route.EndPoint.Length) * 5);

        public bool ProcessOrder()
        {
            if (!CityDatabase.IsValidAddress(Route.StartPoint) || !CityDatabase.IsValidAddress(Route.EndPoint))
            {
                Status = "Відхилено (Невідома адреса)";
                throw new InvalidRouteException($"Адреси '{Route.StartPoint}' або '{Route.EndPoint}' немає в базі міста!");
            }

            if (!CurrentPassenger.CanAffordRide(Price))
            {
                Status = "Відхилено (Фінанси)";
                throw new InsufficientFundsException($"Недостатньо коштів. Вартість {Price} грн.", Price);
            }

            CurrentPassenger.DeductBalance(Price);
            Status = "Подача машини";
            AssignedDriver.State = DriverState.Dispatching;
            Logger.Log($"[Етап 1]: Машина {AssignedDriver.CarModel} виїхала за адресою {Route.StartPoint}.");
            return true;
        }

        public void PassengerBoarding()
        {
            Status = "З пасажиром";
            AssignedDriver.State = DriverState.WithPassenger;
            Logger.Log($"[Етап 2]: Посадка виконана. Прямуємо до {Route.EndPoint}.");
        }

        public void CompleteTrip()
        {
            Status = "Завершено";
            AssignedDriver++;
            Logger.Log($"[Етап 3]: Поїздку завершено.");
        }

        public static Order operator *(Order o, double multiplier) { o.Price *= multiplier; return o; }
        public static Order operator /(Order o, double divider) { if (divider != 0) o.Price /= divider; return o; }
    }
}