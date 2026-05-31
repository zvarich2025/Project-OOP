using System;

namespace TaxiDispatcher
{
    public class Passenger
    {
        // Статичне закрите поле для підрахунку об'єктів
        private static int passengerCount;

        // Властивості, що автоматично реалізуються
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        // Закрита властивість (Private property) - доступна тільки всередині класу
        private string PassengerId { get; set; }

        // 3. Статичний конструктор (Static constructor) - викликається один раз для класу
        static Passenger()
        {
            passengerCount = 0;
            Console.WriteLine("[Статичний конструктор]: Ініціалізовано лічильник пасажирів.");
        }

        // 1. Конструктор без параметрів
        public Passenger() : this("Анонім", "+380000000000") // 6. Виклик іншого конструктора
        {
            Console.WriteLine("[Конструктор без параметрів]: Створено дефолтного пасажира.");
        }

        // 2. Конструктор з параметрами
        public Passenger(string name, string phoneNumber)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            passengerCount++;
            PassengerId = "PASS-" + passengerCount;
            Console.WriteLine($"[Конструктор з параметрами]: Створено пасажира {Name} (ID: {PassengerId})");
        }

        // 5. Конструктор копії
        public Passenger(Passenger other)
        {
            this.Name = other.Name;
            this.PhoneNumber = other.PhoneNumber;
            passengerCount++;
            this.PassengerId = "PASS-COPY-" + passengerCount;
            Console.WriteLine($"[Конструктор копії]: Скопійовано пасажира {other.Name}");
        }

        // Властивість тільки для читання (Read-only property)
        public static int TotalPassengers
        {
            get { return passengerCount; }
        }
    }
}