using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Scanner.StaticChecks
{
    public class DeprecatedLibrariesCheck : StaticVulnerabilityCheck
    {
        public override string Name => "Использование устаревших/уязвимых библиотек";

        // Список устаревших/уязвимых библиотек
        private static readonly List<string> DeprecatedLibraries = new List<string>
{
    "System.Web.HttpUtility",
    "Newtonsoft.Json 9.0.1",
    "log4net 1.2.11",
    "System.Data.SqlClient",
    "HtmlAgilityPack 1.4",
    "ChilkatCrypt2",
    "Microsoft.AspNet.WebApi"
};


        public override Task<bool> CheckAsync(string code)
        {
            Logger.LogInfo("[DeprecatedLibrariesCheck] Начат поиск устаревших библиотек...");

            bool found = DeprecatedLibraries.Any(lib => code.Contains(lib));

            if (found)
            {
                Logger.LogInfo("[DeprecatedLibrariesCheck] Найдена уязвимость: использование устаревшей или уязвимой библиотеки.");
            }
            else
            {
                Logger.LogInfo("[DeprecatedLibrariesCheck] Уязвимость не обнаружена.");
            }

            return Task.FromResult(found);
        }
    }
}
