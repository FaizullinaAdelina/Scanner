using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Scanner
{
    public class BrokenObjectLevelAuthorizationCheck : VulnerabilityCheck
    {
        public override string Name => "Broken Object Level Authorization (BOLA)";

        private readonly List<int> testIds = new List<int> { 1, 2, 3 };

        public override async Task<bool> CheckAsync(string url)
        {
            try
            {
                Logger.LogInfo($"[BOLA] Начинаем проверку {url}");

                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(15);
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Scanner)");

                    foreach (var id in testIds)
                    {
                        string testUrl = $"{url}/{id}";
                        var response = await client.GetAsync(testUrl);
                        string content = await response.Content.ReadAsStringAsync();

                        Logger.LogInfo($"[BOLA] Проверка доступа к id={id}, статус={response.StatusCode}");

                        if (response.IsSuccessStatusCode && !string.IsNullOrEmpty(content))
                        {
                            Logger.LogInfo($"❗ [BOLA] Уязвимость: доступ к ресурсу id={id} получен.");
                            return true;
                        }
                    }

                    Logger.LogInfo("[BOLA] Уязвимость не обнаружена.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"[BOLA] Ошибка при проверке: {ex.Message}");
                return false;
            }
        }
    }
}
