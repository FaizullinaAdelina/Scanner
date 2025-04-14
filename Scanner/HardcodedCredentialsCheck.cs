using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Scanner.StaticChecks
{
    public class HardcodedCredentialsCheck : StaticVulnerabilityCheck
    {
        public override string Name => "Жестко заданные учетные данные в коде";

        public override Task<bool> CheckAsync(string code)
        {
            Logger.LogInfo("[HardcodedCredentialsCheck] Начат поиск жестко заданных учетных данных...");

            // Регулярное выражение для поиска возможных жестко заданных учетных данных
            Regex hardcodedCredentialsPattern = new Regex(
                @"(?i)(password\s*=\s*['""]\w+['""])|" +
                @"(username\s*=\s*['""]\w+['""])|" +
                @"(apikey\s*=\s*['""]\w+['""])|" +
                @"(secret\s*=\s*['""]\w+['""])|" +
                @"(token\s*=\s*['""]\w+['""])",
                RegexOptions.Compiled);

            bool found = hardcodedCredentialsPattern.IsMatch(code);

            if (found)
            {
                Logger.LogInfo("[HardcodedCredentialsCheck] Найдена уязвимость: жестко заданные учетные данные в коде.");
            }
            else
            {
                Logger.LogInfo("[HardcodedCredentialsCheck] Уязвимость не обнаружена.");
            }

            return Task.FromResult(found);
        }
    }
}
