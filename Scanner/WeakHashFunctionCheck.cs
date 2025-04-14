using System.Threading.Tasks;

namespace Scanner.StaticChecks
{
    public class WeakHashFunctionCheck : StaticVulnerabilityCheck
    {
        public override string Name => "Использование небезопасных хеш-функций (MD5/SHA1)";

        public override Task<bool> CheckAsync(string code)
        {
            // Приводим код к нижнему регистру для нормального поиска
            string lowerCode = code.ToLower();

            bool containsMD5 = lowerCode.Contains("md5");
            bool containsSHA1 = lowerCode.Contains("sha1");

            bool isVulnerable = containsMD5 || containsSHA1;

            return Task.FromResult(isVulnerable);
        }
    }
}
