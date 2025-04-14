using System;
using System.Threading.Tasks;

namespace Scanner.StaticChecks
{
    public class InputValidationCheck : StaticVulnerabilityCheck
    {
        public override string Name => "Ошибки обработки ввода данных";

        public override async Task<bool> CheckAsync(string code)
        {
            return await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(code))
                    return false;

                // Приводим код к нижнему регистру для упрощенного поиска
                string loweredCode = code.ToLower();

                // Список потенциально небезопасных мест, где часто отсутствует проверка данных с ввода
                string[] riskyFunctions = { "scanf(", "gets(", "readline(", "console.readline(", "fgets(", "cin >>" };

                foreach (var func in riskyFunctions)
                {
                    if (loweredCode.Contains(func))
                    {
                        // Дополнительно можно проверить, нет ли рядом проверок длины, но это базовый пример
                        return true;
                    }
                }

                // Если ничего подозрительного не найдено
                return false;
            });
        }
    }
}
