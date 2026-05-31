using System;

namespace TaxiDispatcher
{
    public class Order
    {
        // Поля (Властивості)
        public int OrderId { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public double Price { get; set; }
        public string Status { get; set; } // Наприклад: "Створено", "В дорозі", "Завершено"

        // Конструктор
        public Order(int orderId, string startPoint, string endPoint, double price)
        {
            OrderId = orderId;
            StartPoint = startPoint;
            EndPoint = endPoint;
            Price = price;
            Status = "Створено";
        }

        // Методи
        public void UpdateStatus(string newStatus)
        {
            Status = newStatus;
        }
    }
}