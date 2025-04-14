using System;
using System.Threading.Tasks;
using Scanner.StaticChecks; // если интерфейс находится тут

namespace Scanner.StaticChecks
{
    public class FormatStringCheck : StaticVulnerabilityCheck
    {
        public override string Name => "Ошибки форматирования строк";

        public override async Task<bool> CheckAsync(string code)
        {
            // Имитация асинхронной работы
            return await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(code))
                    return false;

                // Приводим весь код к нижнему регистру для упрощения поиска
                string loweredCode = code.ToLower();

                // Примитивный поиск потенциально опасных функций форматирования
                // В C/C++ это printf, sprintf, fprintf и т.д.
                // В C# это string.Format(), Console.WriteLine(), StringBuilder.AppendFormat()

                // Примеры функций, которые могут быть небезопасными без правильного использования
                string[] dangerousFunctions = {
                    "printf(",
                    "sprintf(",
                    "fprintf(",
                    "snprintf(",
                    "vsprintf(",
                    "vfprintf(",
                    "string.format(",
                    "console.writeline(",
                    "appendformat("
                };

                // Простой перебор для поиска использования этих функций
                foreach (var function in dangerousFunctions)
                {
                    if (loweredCode.Contains(function))
                    {
                        // Уязвимость найдена, т.к. используются функции форматирования
                        // без анализа на корректность параметров (в рамках примитивного сканера)
                        return true;
                    }
                }

                return false;
            });
        }
    }
}
