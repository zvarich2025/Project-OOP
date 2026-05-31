using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace TaxiDispatcher
{
    public static class LocalizationManager
    {
        // Словник для зберігання перекладів (Ключ -> Значення)
        private static Dictionary<string, string> _translations = new Dictionary<string, string>();

        // Метод завантаження мовного файлу
        public static void LoadLanguage(string languageCode)
        {
            string filePath = $"{languageCode}.json";

            if (File.Exists(filePath))
            {
                // Зчитуємо весь текст з файлу
                string jsonString = File.ReadAllText(filePath);

                // Перетворюємо текст на словник
                _translations = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);
            }
            else
            {
                Console.WriteLine($"[Системна помилка]: Файл локалізації {filePath} не знайдено.");
            }
        }

        // Метод для отримання тексту за ключем
        public static string GetString(string key)
        {
            // Якщо такий ключ є в словнику - повертаємо його текст
            if (_translations.ContainsKey(key))
            {
                return _translations[key];
            }

            // Якщо ключа немає (наприклад, зробили помилку в назві), повертаємо сам ключ, щоб це було помітно
            return $"[{key}]";
        }
    }
}