using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace TaxiDispatcher
{
    public static class Logger
    {
        private static Dictionary<int, LogEntry> logDictionary = new Dictionary<int, LogEntry>();
        private static int stepCounter = 1;

        public class LogEntry
        {
            public string Time { get; set; }
            public string Message { get; set; }
        }

        public static void Log(string message)
        {
            Console.WriteLine(message);
            logDictionary.Add(stepCounter, new LogEntry
            {
                Time = DateTime.Now.ToString("HH:mm:ss"),
                Message = message
            });
            stepCounter++;
        }

        public static void SaveToJson(string fileName = "simulation_interactive.json")
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            string jsonString = JsonSerializer.Serialize(logDictionary, options);
            File.WriteAllText(fileName, jsonString);
            Console.WriteLine($"\n==================================================");
            Console.WriteLine($"[Система]: Словник логів збережено у файл: {Path.GetFullPath(fileName)}");
        }
    }
}