using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Scanner
{
    public class ClassicSQLInjectionCheck : VulnerabilityCheck
    {
        private static readonly HttpClient _httpClient;

        static ClassicSQLInjectionCheck()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(10);
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
        }

        public override string Name => "Классическая SQL-инъекция";

        public override async Task<bool> CheckAsync(string url)
        {
            try
            {
                string baseUrl = url.Contains("?") ? url + "&test=1" : url + "?test=1";
                Logger.LogInfo($"[ClassicSQLi] Базовый URL: {baseUrl}");

                var baselineResponse = await _httpClient.GetAsync(baseUrl);
                string baselineContent = await baselineResponse.Content.ReadAsStringAsync();

                if (baselineContent.Length < 100)
                {
                    Logger.LogInfo("[ClassicSQLi] Базовый контент слишком короткий для анализа.");
                    return false;
                }

                string[] payloads = {
                    "%20OR%201=1--",
                    "'%20OR%201=1--",
                    ")%20OR%201=1--"
                };

                foreach (var payload in payloads)
                {
                    string testUrl = $"{baseUrl}{payload}";
                    var testResponse = await _httpClient.GetAsync(testUrl);
                    string testContent = await testResponse.Content.ReadAsStringAsync();

                    bool isErrorPresent = testContent.ToLower().Contains("error") || testContent.ToLower().Contains("warning");
                    double relativeDifference = Math.Abs(testContent.Length - baselineContent.Length) / (double)baselineContent.Length;

                    if (isErrorPresent || relativeDifference > 0.1)
                    {
                        Logger.LogInfo($"[ClassicSQLi] Уязвимость найдена! Payload: {payload}");
                        return true;
                    }
                }

                Logger.LogInfo("[ClassicSQLi] Уязвимость не обнаружена.");
                return false;
            }
            catch (TaskCanceledException)
            {
                Logger.LogError("[ClassicSQLi] Таймаут при выполнении запроса.");
                return false;
            }
            catch (Exception ex)
            {
                Logger.LogError($"[ClassicSQLi] Ошибка: {ex.Message}");
                return false;
            }
        }
    }
}
