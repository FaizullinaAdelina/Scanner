using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Scanner.StaticChecks
{
    public class UnsafeMemoryFunctionsCheck : StaticVulnerabilityCheck
    {
        public override string Name => "Использование небезопасных функций работы с памятью (небезопасные указатели, P/Invoke, DllImport)";

        public override Task<bool> CheckAsync(string code)
        {
            Logger.LogInfo("[UnsafeMemoryFunctionsCheck] Поиск небезопасных операций с памятью...");

            // Регулярное выражение для поиска подозрительных вызовов
            string[] unsafePatterns =
            {
                @"\bgets\s*\(",          // gets()
                @"\bstrcpy\s*\(",        // strcpy()
                @"\bstrcat\s*\(",        // strcat()
                @"\bmemcpy\s*\(",        // memcpy() через DllImport
                @"\bMarshal\.Copy\s*\(", // Marshal.Copy()
                @"\bBuffer\.BlockCopy\s*\(", // Buffer.BlockCopy()
                @"\bstackalloc\s+\w+",   // stackalloc без проверок
                @"\bunsafe\s*\{",        // Использование unsafe-блока
                @"\bDllImport\s*\("      // Импорт функций C из библиотеки
            };

            bool found = false;

            foreach (var pattern in unsafePatterns)
            {
                if (Regex.IsMatch(code, pattern))
                {
                    found = true;
                    Logger.LogInfo($"[UnsafeMemoryFunctionsCheck] Обнаружена потенциальная уязвимость: {pattern}");
                }
            }

            if (!found)
            {
                Logger.LogInfo("[UnsafeMemoryFunctionsCheck] Уязвимость не обнаружена.");
            }

            return Task.FromResult(found);
        }
    }
}
