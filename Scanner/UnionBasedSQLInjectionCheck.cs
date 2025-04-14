using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Scanner
{
    public class UnionBasedSQLInjectionCheck : VulnerabilityCheck
    {
        private static readonly HttpClient _httpClient;

        static UnionBasedSQLInjectionCheck()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
        }

        public override string Name => "Union-based SQL Injection";

        // UnionBasedSQLInjectionCheck.cs
        public override async Task<bool> CheckAsync(string url)
        {
            try
            {
                string baseUrl = url.Contains("?") ? url + "&test=1" : url + "?test=1";
                var baselineResponse = await _httpClient.GetAsync(baseUrl);
                string baselineContent = await baselineResponse.Content.ReadAsStringAsync();

                // Перебор от 1 до 10 столбцов
                for (int i = 1; i <= 10; i++)
                {
                    string nulls = string.Join(",", Enumerable.Repeat("null", i));
                    string payload = Uri.EscapeDataString($" UNION SELECT {nulls}--");
                    string testUrl = $"{baseUrl}{payload}";

                    var testResponse = await _httpClient.GetAsync(testUrl);
                    string testContent = await testResponse.Content.ReadAsStringAsync();

                    if (testContent != baselineContent) return true;
                    if (testContent.Contains("超出预期结果") || testContent.Contains("unexpected column"))
                        return true;
                }
            }
            catch { }
            return false;
        }
    }
}