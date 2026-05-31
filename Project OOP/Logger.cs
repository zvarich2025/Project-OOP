using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace TaxiDispatcher
{
    public static class Logger
    {
        // ЗМІНЕНО: Тепер використовуємо Dictionary (Словник) замість List
        // Ключ (int) - це номер повідомлення, Значення (LogEntry) - це час і текст
        private static Dictionary<int, LogEntry> logDictionary = new Dictionary<int, LogEntry>();

        // Лічильник для створення унікальних ключів словника
        private static int stepCounter = 1;

        public class LogEntry
        {
            public string Time { get; set; }
            public string Message { get; set; }
        }

        public static void Log(string message)
        {
            Console.WriteLine(message);

            // Додаємо запис у словник з унікальним номером кроку
            logDictionary.Add(stepCounter, new LogEntry
            {
                Time = DateTime.Now.ToString("HH:mm:ss"),
                Message = message
            });

            stepCounter++; // Збільшуємо номер для наступного запису
        }

        public static void SaveToJson(string fileName = "simulation_dictionary.json")
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            // Серіалізуємо саме словник
            string jsonString = JsonSerializer.Serialize(logDictionary, options);
            File.WriteAllText(fileName, jsonString);

            Console.WriteLine($"\n==================================================");
            Console.WriteLine($"[Система]: Словник логів збережено у файл: {Path.GetFullPath(fileName)}");
        }
    }
}