using System;
using System.Threading.Tasks;
using Scanner.StaticChecks; // если интерфейс находится тут

namespace Scanner.StaticChecks
{
    public class SystemCallCheck : StaticVulnerabilityCheck
    {
        public override string Name => "Вызов system()";

        public override async Task<bool> CheckAsync(string code)
        {
            // Имитация асинхронной работы
            return await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(code))
                    return false;

                // Примитивный поиск вызова system()
                string loweredCode = code.ToLower();

                return loweredCode.Contains("system(");
            });
        }
    }
}
