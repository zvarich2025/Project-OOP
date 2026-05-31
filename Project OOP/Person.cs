using System;

namespace TaxiDispatcher
{
    // Абстрактний базовий клас для Пасажира та Водія
    public abstract class Person
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public Person(string name, string phoneNumber)
        {
            Name = name;
            PhoneNumber = phoneNumber;
        }

        // Абстрактний метод, який обов'язково треба реалізувати в нащадках
        public abstract void PrintRole();
    }
}