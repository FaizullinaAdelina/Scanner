using System;
using System.Threading.Tasks;

namespace Scanner.StaticChecks
{
    public class BufferOverflowCheck : StaticVulnerabilityCheck
    {
        public override string Name => "Переполнение буфера";

        public override async Task<bool> CheckAsync(string code)
        {
            return await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(code))
                    return false;

                // Приводим код к нижнему регистру для упрощённого поиска
                string loweredCode = code.ToLower();

                // Список функций, которые могут привести к переполнению буфера
                string[] dangerousFunctions = { "strcpy(", "strcat(", "sprintf(", "vsprintf(", "gets(", "scanf(" };

                // Поиск использования опасных функций
                foreach (var func in dangerousFunctions)
                {
                    if (loweredCode.Contains(func))
                        return true;
                }

                // Если ничего не найдено
                return false;
            });
        }
    }
}
