using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Scanner
{
    public class ReflectedXSSCheck : VulnerabilityCheck
    {
        private static readonly HttpClient _httpClient;

        static ReflectedXSSCheck()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
        }

        public override string Name => "Reflected XSS";

        public override async Task<bool> CheckAsync(string url)
        {
            try
            {
                // Формируем baseline URL: используем параметр q=1
                string baselineUrl = url.Contains("?") ? url + "&q=1" : url + "?q=1";
                var baselineResponse = await _httpClient.GetAsync(baselineUrl);
                string baselineContent = await baselineResponse.Content.ReadAsStringAsync();

                // Инъекционный payload для XSS
                string payload = "<script>alert('XSS')</script>";
                string testUrl = url.Contains("?")
                    ? url + "&q=" + Uri.EscapeDataString(payload)
                    : url + "?q=" + Uri.EscapeDataString(payload);

                var testResponse = await _httpClient.GetAsync(testUrl);
                string testContent = await testResponse.Content.ReadAsStringAsync();

                // Если в тестовом ответе присутствует payload, а в baseline его нет — отражённый XSS обнаружен
                if (testContent.Contains(payload) && !baselineContent.Contains(payload))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ReflectedXSSCheck error: {ex.Message}");
            }
            return false;
        }
    }
}
