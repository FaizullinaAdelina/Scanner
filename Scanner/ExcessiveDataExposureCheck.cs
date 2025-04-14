using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Scanner
{
    public class ExcessiveDataExposureCheck : VulnerabilityCheck
    {
        public override string Name => "Excessive Data Exposure";

        private readonly List<string> sensitiveFields = new List<string>
        {
            "password", "token", "ssn", "creditcard", "credit_card", "cvv", "authorization", "user_id"
        };

        public override async Task<bool> CheckAsync(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(15);
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Scanner)");

                    Console.WriteLine($"Проверка на Excessive Data Exposure: {url}");

                    var response = await client.GetAsync(url).ConfigureAwait(false);
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    JObject json;
                    try
                    {
                        json = JObject.Parse(content);
                    }
                    catch
                    {
                        Console.WriteLine("Ответ не является JSON.");
                        return false;
                    }

                    foreach (var prop in json.Properties())
                    {
                        var propName = prop.Name.ToLower();
                        if (sensitiveFields.Exists(field => propName.Contains(field)))
                        {
                            Console.WriteLine($"❗ Найдено чувствительное поле: {prop.Name}");
                            return true;
                        }
                    }

                    Console.WriteLine("Избыточная передача данных не обнаружена.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ExcessiveDataExposureCheck] Ошибка: {ex.Message}");
                return false;
            }
        }
    }
}
