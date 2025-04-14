using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Scanner;

public class IDORCheck : VulnerabilityCheck
{
    private static readonly HttpClient _httpClient = new HttpClient();

    public override string Name => "Insecure Direct Object References (IDOR)";

    public override async Task<bool> CheckAsync(string url)
    {
        try
        {
            Logger.LogInfo($"[IDOR] Проверяем {url}");

            var testUrls = new List<string>
            {
                $"{url}/profile?id=1",
                $"{url}/profile?id=2",
                $"{url}/profile?id=9999",
                $"{url}/profile/1",
                $"{url}/profile/2",
                $"{url}/profile/9999",
                $"{url}/document/1",
                $"{url}/document/2",
                $"{url}/document/9999"
            };

            string baselineContent = await GetContent($"{url}/profile?id=1");

            foreach (var testUrl in testUrls)
            {
                Logger.LogInfo($"[IDOR] Тестируем: {testUrl}");

                string responseContent = await GetContent(testUrl);

                if (!string.IsNullOrEmpty(responseContent) && responseContent != baselineContent)
                {
                    Logger.LogInfo($"[IDOR] Уязвимость найдена на {testUrl}");
                    return true;
                }
            }

            Logger.LogInfo("[IDOR] Уязвимость не найдена.");
            return false;
        }
        catch (Exception ex)
        {
            Logger.LogError($"[IDOR] Ошибка: {ex.Message}");
            return false;
        }
    }

    private async Task<string> GetContent(string url)
    {
        try
        {
            Logger.LogInfo($"[IDOR] Запрашиваем {url}");
            var response = await _httpClient.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            Logger.LogError($"[IDOR] Ошибка при запросе {url}: {ex.Message}");
            return string.Empty;
        }
    }
}
