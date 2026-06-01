using System;
using System.Linq;
using System.Text;

namespace TaxiDispatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            LocalizationManager.LoadLanguage("uk");
            CityDatabase.UpdateFromInternet();

            Logger.Log(LocalizationManager.GetString("StudentInfo"));
            Logger.Log("Версія 7.2 (База клієнтів TXT, Валідація, Чорні списки)\n");
            Logger.Log(LocalizationManager.GetString("SimulationStart") + "\n");

            DispatcherSystem dispatcher = new DispatcherSystem("АТП SmartCity");
            dispatcher.AddDispatcher(new DispatcherEmployee("Марія", "101", DispatcherRole.OrderReceiver));
            dispatcher.AddDispatcher(new DispatcherEmployee("Олег", "102", DispatcherRole.FleetManager));

            Logger.Log("\n[Система]: Формування автопарку на зміну...");
            dispatcher.AddDriverToStand(new Driver("Микола (Порушник)", "050111", "Skoda Octavia", "AA1111BB", TaxiClass.Economy));
            dispatcher.AddDriverToStand(new Driver("Василь", "050222", "Renault Logan", "BC2222CC", TaxiClass.Economy));
            dispatcher.AddDriverToStand(new Driver("Олена", "050333", "Toyota Camry", "KA3333XX", TaxiClass.Comfort));
            dispatcher.AddDriverToStand(new Driver("Дмитро", "050444", "Volkswagen Passat", "AX4444YY", TaxiClass.Comfort));
            dispatcher.AddDriverToStand(new Driver("Степан", "050555", "Mercedes-Maybach S-Class", "VIP001", TaxiClass.VIP));
            dispatcher.AddDriverToStand(new Driver("Артем", "050666", "Porsche Panamera", "VIP002", TaxiClass.VIP));
            Console.WriteLine("\n------------------------------------------------");

            // --- 1. ВАЛІДАЦІЯ ІМЕНІ ---
            string passName = "";
            while (true)
            {
                Console.Write(LocalizationManager.GetString("PromptName"));
                passName = Console.ReadLine()?.Trim();

                if (!string.IsNullOrEmpty(passName) && passName.All(c => char.IsLetter(c) || c == ' ' || c == '\'' || c == '-'))
                    break;

                Console.WriteLine("[Помилка]: Ім'я має містити ЛИШЕ літери. Спробуйте ще раз.\n");
            }

            // --- 2. ВАЛІДАЦІЯ ТЕЛЕФОНУ ---
            string passPhone = "";
            while (true)
            {
                Console.Write(LocalizationManager.GetString("PromptPhone"));
                passPhone = Console.ReadLine()?.Trim();

                string digitsToCheck = passPhone.StartsWith("+") ? passPhone.Substring(1) : passPhone;

                if (!string.IsNullOrEmpty(digitsToCheck) && digitsToCheck.All(char.IsDigit) && passPhone.Length >= 10)
                    break;

                Console.WriteLine("[Помилка]: Номер телефону має містити ЛИШЕ цифри (мінімум 10 символів). Спробуйте ще раз.\n");
            }

            Passenger currentPassenger = new Passenger(passName, passPhone, 1500.0);
            Logger.Log($"\n[Система]: Зареєстровано пасажира {currentPassenger.Name}. Баланс: 1500 грн.");

            // --- НОВЕ: ЗАПИСУЄМО КЛІЄНТА У TXT ФАЙЛ ---
            Logger.SaveClientToTxt(currentPassenger);
            Console.WriteLine();

            // --- 3. ВАЛІДАЦІЯ МАРШРУТУ ---
            string startRoute = "";
            while (true)
            {
                Console.Write(LocalizationManager.GetString("PromptStart"));
                startRoute = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(startRoute)) break;
                Console.WriteLine("[Помилка]: Адреса відправлення не може бути порожньою.\n");
            }

            string endRoute = "";
            while (true)
            {
                Console.Write(LocalizationManager.GetString("PromptEnd"));
                endRoute = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(endRoute))
                    Console.WriteLine("[Помилка]: Адреса призначення не може бути порожньою.\n");
                else if (startRoute.Equals(endRoute, StringComparison.OrdinalIgnoreCase))
                    Console.WriteLine("[Помилка]: Місце відправлення та призначення НЕ МОЖУТЬ співпадати! Введіть іншу адресу.\n");
                else
                    break;
            }

            Console.Write("Коментар для водія (наприклад, 'Буду з котом' або залиште порожнім): ");
            string passComment = Console.ReadLine();

            // --- 4. ВАЛІДАЦІЯ КЛАСУ ТАКСІ ---
            TaxiClass selectedClass = TaxiClass.Economy;
            while (true)
            {
                Console.Write(LocalizationManager.GetString("PromptClass"));
                string classInput = Console.ReadLine();

                if (int.TryParse(classInput, out int classChoice) && classChoice >= 1 && classChoice <= 3)
                {
                    selectedClass = (TaxiClass)classChoice;
                    break;
                }
                Console.WriteLine(LocalizationManager.GetString("ErrorInvalidInput"));
            }

            // --- 5. ОБРОБКА ЗАМОВЛЕННЯ ---
            Logger.Log($"\n[Система]: Виконується пошук авто класу {selectedClass}...");

            try
            {
                Driver assignedDriver = dispatcher.GetNextAvailableDriver(selectedClass);
                Order newOrder = new Order(101, currentPassenger, assignedDriver, startRoute, endRoute, OrderSource.Mobile, passComment);
                dispatcher.DispatchLog(newOrder);
            }
            catch (Exception ex)
            {
                Logger.Log($"[ПЕРЕХОПЛЕНО ПОМИЛКУ]: {ex.Message}");
            }
            finally
            {
                dispatcher.GenerateShiftReport();
                Logger.Log(LocalizationManager.GetString("SimulationFinish"));
                Logger.SaveToJson("simulation_live.json");
            }

            Console.ReadLine();
        }
    }
}