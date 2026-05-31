using System;

namespace TaxiDispatcher
{
    public abstract class Person
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public Person(string name, string phoneNumber)
        {
            Name = name;
            PhoneNumber = phoneNumber;
        }

        // Цей рядок обов'язковий!
        public abstract void PrintRole();
    }
}