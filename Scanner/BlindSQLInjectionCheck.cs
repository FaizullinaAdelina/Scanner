using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Web;

namespace Scanner
{
    public class BlindSQLInjectionCheck : VulnerabilityCheck
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        static BlindSQLInjectionCheck()
        {
            _httpClient.Timeout = TimeSpan.FromSeconds(20);
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
        }

        public override string Name => "Blind SQL Injection";

        public override async Task<bool> CheckAsync(string url)
        {
            try
            {
                Logger.LogInfo($"[BlindSQLi] Начинаем проверку {url}");

                var uriBuilder = new UriBuilder(url);
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);

                string targetParam = "cat";
                if (string.IsNullOrEmpty(query[targetParam]))
                {
                    Logger.LogInfo($"[BlindSQLi] Целевой параметр '{targetParam}' не найден.");
                    return false;
                }

                string originalValue = query[targetParam];

                string[] testPayloads = {
                    $"{originalValue} AND 1=1--",
                    $"{originalValue} AND 1=2--"
                };

                query[targetParam] = originalValue;
                uriBuilder.Query = query.ToString();
                string baselineContent = await GetContent(uriBuilder.ToString());

                bool trueConditionResult = false;
                bool falseConditionResult = false;

                foreach (var payload in testPayloads)
                {
                    query[targetParam] = payload;
                    uriBuilder.Query = query.ToString();
                    string testContent = await GetContent(uriBuilder.ToString());

                    Logger.LogInfo($"[BlindSQLi] Проверяем payload: {payload}");

                    if (payload.Contains("1=1--"))
                        trueConditionResult = AnalyzeContent(testContent, baselineContent);
                    else
                        falseConditionResult = AnalyzeContent(testContent, baselineContent, isFalseCondition: true);
                }

                bool result = trueConditionResult && falseConditionResult;
                Logger.LogInfo($"[BlindSQLi] Результат: {(result ? "Уязвимость обнаружена!" : "Уязвимость не найдена.")}");
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError($"[BlindSQLi] Ошибка: {ex.Message}");
                return false;
            }
        }

        private async Task<string> GetContent(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError($"[BlindSQLi] Ошибка при запросе {url}: {ex.Message}");
                return string.Empty;
            }
        }

        private bool AnalyzeContent(string testContent, string baselineContent, bool isFalseCondition = false)
        {
            bool contentChanged = testContent != baselineContent;

            if (!isFalseCondition)
            {
                bool hasProducts = testContent.Contains("product.php?pic=");
                bool hasExpectedText = testContent.Contains("Graffity");

                return contentChanged && hasProducts && hasExpectedText;
            }

            return contentChanged && !testContent.Contains("product.php?pic=");
        }
    }
}
