using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace TaxiDispatcher
{
    public static class Logger
    {
        // Список для зберігання всіх повідомлень
        private static List<LogEntry> logEntries = new List<LogEntry>();

        // Структура одного запису в лозі JSON
        public class LogEntry
        {
            public string Time { get; set; }
            public string Message { get; set; }
        }

        // Заміна стандартного Console.WriteLine
        public static void Log(string message)
        {
            Console.WriteLine(message); // Виводимо текст у консоль
            logEntries.Add(new LogEntry { Time = DateTime.Now.ToString("HH:mm:ss"), Message = message }); // Зберігаємо для JSON
        }

        // Збереження у файл
        public static void SaveToJson(string fileName = "simulation_result.json")
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true, // Робить JSON красивим (з відступами)
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping // Зберігає українські літери нормальними, а не кодами
            };

            string jsonString = JsonSerializer.Serialize(logEntries, options);
            File.WriteAllText(fileName, jsonString);

            // Це повідомлення виводимо тільки в консоль
            Console.WriteLine($"\n==================================================");
            Console.WriteLine($"[Система]: Весь протокол успішно збережено у файл: {Path.GetFullPath(fileName)}");
        }
    }
}