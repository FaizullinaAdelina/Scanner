using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Scanner
{
    public class CSRFMissingTokenCheck : VulnerabilityCheck
    {
        private static readonly HttpClient _httpClient;

        static CSRFMissingTokenCheck()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
        }

        public override string Name => "Cross-Site Request Forgery (CSRF) - Missing Token";

        public override async Task<bool> CheckAsync(string url)
        {
            try
            {
                string targetUrl = url.EndsWith("/") ? url + "update-email" : url + "/update-email";
                var response = await _httpClient.GetAsync(targetUrl).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var lowerContent = content.ToLower();

                if (!lowerContent.Contains("name='csrf_token'") && !lowerContent.Contains("name=\"csrf_token\""))
                {
                    Console.WriteLine($"❗ CSRF токен отсутствует на {targetUrl}");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CSRFMissingTokenCheck] Ошибка: {ex.Message}");
            }

            return false;
        }
    }
}
