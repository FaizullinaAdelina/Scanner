using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Collections.Generic;
using Scanner;

public class OpenRedirectCheck : VulnerabilityCheck
{
    private static readonly HttpClient _httpClient = new HttpClient();

    public override string Name => "Open Redirect";

    public override async Task<bool> CheckAsync(string url)
    {
        try
        {
            Logger.LogInfo($"[Open Redirect] Проверяем {url}");

            var testUrls = new List<string>
            {
                $"{url}/redirect?url=https://evil.com",
                $"{url}/redirect?next=https://evil.com",
                $"{url}/redirect?to=https://evil.com",
                $"{url}/redirect?destination=https://evil.com",
            };

            foreach (var testUrl in testUrls)
            {
                Logger.LogInfo($"[Open Redirect] Тестируем: {testUrl}");

                var response = await _httpClient.GetAsync(testUrl);
                string finalUrl = response.RequestMessage?.RequestUri.ToString() ?? "";

                if (finalUrl.Contains("evil.com"))
                {
                    Logger.LogInfo($"[Open Redirect] Уязвимость найдена на {testUrl}");
                    return true;
                }
            }

            Logger.LogInfo("[Open Redirect] Уязвимость не найдена.");
            return false;
        }
        catch (Exception ex)
        {
            Logger.LogError($"[Open Redirect] Ошибка: {ex.Message}");
            return false;
        }
    }
}
