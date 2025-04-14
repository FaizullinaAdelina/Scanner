using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Scanner
{
    public class ErrorBasedSQLInjectionCheck : VulnerabilityCheck
    {
        private static readonly HttpClient _httpClient;

        static ErrorBasedSQLInjectionCheck()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
        }

        public override string Name => "Error-based SQL Injection";

        public override async Task<bool> CheckAsync(string url)
        {
            try
            {
                var uriBuilder = new UriBuilder(url);
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query["cat"] = "1";
                uriBuilder.Query = query.ToString();
                string baselineUrl = uriBuilder.ToString();

                File.AppendAllText("scan.log", $"[{DateTime.Now}] Baseline URL: {baselineUrl}\n");

                var baselineResponse = await _httpClient.GetAsync(baselineUrl).ConfigureAwait(false);
                var baselineContent = await baselineResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

                bool baselineHasError = ContainsSqlError(baselineContent);

                string[] payloads = { "'", "\"", "';", "\";" };

                foreach (var payload in payloads)
                {
                    query["cat"] = "1" + payload;
                    uriBuilder.Query = query.ToString();
                    string testUrl = uriBuilder.ToString();

                    File.AppendAllText("scan.log", $"[{DateTime.Now}] Testing: {testUrl}\n");

                    var response = await _httpClient.GetAsync(testUrl).ConfigureAwait(false);
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    if (ContainsSqlError(content) && !baselineHasError)
                    {
                        File.AppendAllText("scan.log", $"[{DateTime.Now}] ❗ Уязвимость обнаружена с payload: {payload}\n");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText("scan.log", $"[{DateTime.Now}] Error: {ex.Message}\n");
            }

            return false;
        }

        private bool ContainsSqlError(string content)
        {
            var lowerContent = content.ToLower();
            return lowerContent.Contains("error") ||
                   lowerContent.Contains("exception") ||
                   lowerContent.Contains("syntax");
        }
    }
}
