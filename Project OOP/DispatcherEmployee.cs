using System;

namespace TaxiDispatcher
{
    // Диспетчер - це також Person (Успадкування)
    public class DispatcherEmployee : Person
    {
        public DispatcherRole Role { get; set; }
        public int HandledOrders { get; set; }

        public DispatcherEmployee(string name, string phone, DispatcherRole role) : base(name, phone)
        {
            Role = role;
            HandledOrders = 0;
        }

        public override void PrintRole() => Logger.Log($"[Бригада]: Диспетчер {Name}, Роль: {Role}");
    }
}