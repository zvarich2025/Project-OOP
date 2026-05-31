using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace TaxiDispatcher
{
    public static class LocalizationManager
    {
        private static Dictionary<string, string> _translations = new Dictionary<string, string>();

        public static void LoadLanguage(string languageCode)
        {
            string filePath = $"{languageCode}.json";
            try
            {
                string jsonString = File.ReadAllText(filePath);
                _translations = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"[КРИТИЧНО]: Файл локалізації '{filePath}' не знайдено! {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ПОМИЛКА]: Невідома помилка локалізації: {ex.Message}");
            }
        }

        public static string GetString(string key)
        {
            if (_translations.ContainsKey(key)) return _translations[key];
            return $"[{key}]";
        }
    }
}