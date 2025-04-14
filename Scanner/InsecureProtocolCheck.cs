using System;
using System.Threading.Tasks;

namespace Scanner
{
    public class InsecureProtocolCheck : VulnerabilityCheck
    {
        public override string Name => "Insecure HTTP Protocol";

        public override async Task<bool> CheckAsync(string url)
        {
            // Если URL не начинается с "https://", уязвимость обнаружена
            if (!url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Сайт использует HTTP вместо HTTPS.");
                return true;
            }
            return false;
        }
    }
}
