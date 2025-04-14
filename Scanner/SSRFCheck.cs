using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Web;
using System.Collections.Generic;

namespace Scanner
{
    public class SSRFCheck : VulnerabilityCheck
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        static SSRFCheck()
        {
            _httpClient.Timeout = TimeSpan.FromSeconds(20);
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
        }

        public override string Name => "Server-Side Request Forgery (SSRF)";

        public override async Task<bool> CheckAsync(string url)
        {
            try
            {
                Logger.LogInfo($"[SSRF] Начинаем проверку {url}");

                var uriBuilder = new UriBuilder(url);
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);

                string targetParam = "url"; // Основное название параметра SSRF

                // Если параметр отсутствует, пробуем добавить его сами
                if (string.IsNullOrEmpty(query[targetParam]))
                {
                    Logger.LogInfo($"[SSRF] Параметр '{targetParam}' отсутствует, пробуем добавить его.");

                    string[] testParams = { "url", "link", "target" };

                    foreach (var param in testParams)
                    {
                        string testUrl = $"{url}?{param}=http://example.com";
                        Logger.LogInfo($"[SSRF] Тестируем: {testUrl}");

                        string response = await GetContent(testUrl);
                        if (!string.IsNullOrEmpty(response) && !response.Contains("Ошибка"))
                        {
                            Logger.LogInfo($"[SSRF] Уязвимость найдена на {testUrl}");
                            return true;
                        }
                    }

                    Logger.LogInfo("[SSRF] Уязвимость не найдена.");
                    return false;
                }

                string originalValue = query[targetParam];

                string[] testUrls =
                {
                    "http://localhost",
                    "http://127.0.0.1",
                    "http://169.254.169.254", // AWS/GCP метаданные
                    "http://192.168.1.1",
                    "http://example.com" // Проверка нормального запроса
                };

                query[targetParam] = originalValue;
                uriBuilder.Query = query.ToString();
                string baselineContent = await GetContent(uriBuilder.ToString());

                bool isVulnerable = false;

                foreach (var testUrl in testUrls)
                {
                    query[targetParam] = testUrl;
                    uriBuilder.Query = query.ToString();
                    string testContent = await GetContent(uriBuilder.ToString());

                    Logger.LogInfo($"[SSRF] Проверяем URL: {testUrl}");

                    if (!string.IsNullOrEmpty(testContent) && !testContent.Contains("Ошибка"))
                    {
                        return true;
                    }
                    if (Math.Abs(testContent.Length - baselineContent.Length) > 20)
                    {
                        return true;
                    }
                }

                Logger.LogInfo($"[SSRF] Результат: {(isVulnerable ? "Уязвимость обнаружена!" : "Уязвимость не найдена.")}");
                return isVulnerable;
            }
            catch (Exception ex)
            {
                Logger.LogError($"[SSRF] Ошибка: {ex.Message}");
                return false;
            }
        }

        private async Task<string> GetContent(string url)
        {
            try
            {
                Logger.LogInfo($"[SSRF] Запрашиваем {url}");
                var response = await _httpClient.GetAsync(url);
                string content = await response.Content.ReadAsStringAsync();
                Logger.LogInfo($"[SSRF] Ответ {url}: {content.Substring(0, Math.Min(content.Length, 200))}...");
                return content;
            }
            catch (Exception ex)
            {
                Logger.LogError($"[SSRF] Ошибка при запросе {url}: {ex.Message}");
                return string.Empty;
            }
        }
    }
}
