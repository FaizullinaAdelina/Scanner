using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Scanner
{
    public class MissingSecurityHeadersCheck : VulnerabilityCheck
    {
        private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(15);

        private struct HeaderCheck
        {
            public string Name;
            public Func<string, bool> Validator;

            public HeaderCheck(string name, Func<string, bool> validator)
            {
                Name = name;
                Validator = validator;
            }
        }

        private static readonly HeaderCheck[] RequiredHeaders =
        {
            new HeaderCheck("Strict-Transport-Security", value => value.Contains("max-age")),
            new HeaderCheck("Content-Security-Policy", _ => true),
            new HeaderCheck("X-Content-Type-Options", value => value.Equals("nosniff", StringComparison.OrdinalIgnoreCase))
        };

        public override string Name => "Missing Security Headers";

        public override async Task<bool> CheckAsync(string url)
        {
            if (!url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                return false;

            try
            {
                using (var client = new HttpClient(new HttpClientHandler { AllowAutoRedirect = false }) { Timeout = Timeout })
                {
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

                    var response = await CheckRedirectChain(client, url);
                    if (response == null) return false;

                    var headers = new HashSet<string>(
                        response.Headers.Select(h => h.Key),
                        StringComparer.OrdinalIgnoreCase
                    );

                    foreach (var header in RequiredHeaders)
                    {
                        if (!headers.Contains(header.Name))
                        {
                            LogMissingHeader(header.Name);
                            return true;
                        }

                        var headerValue = response.Headers.GetValues(header.Name).FirstOrDefault();
                        if (!header.Validator(headerValue))
                        {
                            LogInvalidHeader(header.Name, headerValue);
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Check error: {ex.Message}");
                return false;
            }
        }

        private async Task<HttpResponseMessage> CheckRedirectChain(HttpClient client, string originalUrl)
        {
            var currentUrl = originalUrl;
            for (int i = 0; i < 5; i++)
            {
                var response = await client.GetAsync(currentUrl, HttpCompletionOption.ResponseHeadersRead);

                if ((int)response.StatusCode >= 300 && (int)response.StatusCode < 400)
                {
                    currentUrl = response.Headers.Location?.ToString();
                    continue;
                }

                return response;
            }
            return null;
        }

        private void LogMissingHeader(string headerName)
        {
            Console.WriteLine($"Отсутствует обязательный заголовок безопасности: {headerName}");
        }

        private void LogInvalidHeader(string headerName, string value)
        {
            Console.WriteLine($"Некорректное значение заголовка {headerName}: {value}");
        }
    }
}