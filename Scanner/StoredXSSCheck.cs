using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Scanner
{
    public class StoredXSSCheck : VulnerabilityCheck
    {
        private static readonly HttpClient _httpClient;

        static StoredXSSCheck()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
        }

        public override string Name => "Stored XSS";

        public override async Task<bool> CheckAsync(string url)
        {
            try
            {
                // Определяем payload для XSS
                string payload = "<script>alert('StoredXSS')</script>";

                // Формируем URL для отправки комментария через GET
                // Если в URL уже есть параметры, добавляем с помощью &, иначе — с помощью ?
                string submissionUrl = url.Contains("?")
                    ? $"{url}&comment={Uri.EscapeDataString(payload)}"
                    : $"{url}?comment={Uri.EscapeDataString(payload)}";

                // Отправляем запрос для сохранения комментария
                var submissionResponse = await _httpClient.GetAsync(submissionUrl);
                // Небольшая задержка для обработки и сохранения комментария
                await Task.Delay(500);

                // Получаем страницу, где выводятся комментарии (в нашем случае — тот же URL)
                var getResponse = await _httpClient.GetAsync(url);
                string getContent = await getResponse.Content.ReadAsStringAsync();

                // Если сохранённый комментарий (payload) присутствует в выводе, уязвимость обнаружена
                if (getContent.Contains(payload))
                {
                    Console.WriteLine("Stored XSS обнаружена");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"StoredXSSCheck error: {ex.Message}");
            }
            return false;
        }
    }
}
