using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Scanner
{
    public class ClickjackingCheck : VulnerabilityCheck
    {
        public override string Name => "Clickjacking Protection Missing";

        public override async Task<bool> CheckAsync(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(15);
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

                    var response = await client.GetAsync(url);

                    Console.WriteLine("------ Заголовки ответа ------");
                    foreach (var header in response.Headers)
                    {
                        Console.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
                    }

                    bool hasXFrameOptions = response.Headers.TryGetValues("X-Frame-Options", out var xFrameValues);
                    bool hasCSP = response.Headers.TryGetValues("Content-Security-Policy", out var cspValues);

                    if (!hasXFrameOptions && !hasCSP)
                    {
                        Console.WriteLine("Clickjacking защита отсутствует!");
                        return true; // Уязвимость найдена
                    }

                    if (hasCSP)
                    {
                        string csp = cspValues.FirstOrDefault() ?? "";
                        if (csp.IndexOf("frame-ancestors", StringComparison.OrdinalIgnoreCase) < 0)
                        {
                            Console.WriteLine("CSP найден, но без frame-ancestors. Clickjacking возможен.");
                            return true; // Уязвимость найдена
                        }
                    }

                    Console.WriteLine("Clickjacking защита найдена.");
                    return false; // Всё ок
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при проверке Clickjacking: {ex.Message}");
                return false;
            }
        }
    }
}
