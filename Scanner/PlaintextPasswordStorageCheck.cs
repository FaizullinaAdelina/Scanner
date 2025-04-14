using System.Threading.Tasks;

namespace Scanner.StaticChecks
{
    public class PlaintextPasswordStorageCheck : StaticVulnerabilityCheck
    {
        public override string Name => "Хранение паролей в открытом виде";

        public override Task<bool> CheckAsync(string code)
        {
            // Приводим код к нижнему регистру, чтобы не зависеть от регистра символов
            string lowerCode = code.ToLower();

            // Признаки плохого хранения паролей:
            bool containsPasswordField = lowerCode.Contains("create table") &&
                                         lowerCode.Contains("password");

            bool insertsPasswordDirectly = lowerCode.Contains("insert into") &&
                                           lowerCode.Contains("password") &&
                                           // Нет хеширования при добавлении пароля
                                           !(lowerCode.Contains("hash") ||
                                             lowerCode.Contains("encrypt") ||
                                             lowerCode.Contains("bcrypt") ||
                                             lowerCode.Contains("argon2") ||
                                             lowerCode.Contains("scrypt"));

            bool updatesPasswordDirectly = lowerCode.Contains("update") &&
                                           lowerCode.Contains("password") &&
                                           !(lowerCode.Contains("hash") ||
                                             lowerCode.Contains("encrypt") ||
                                             lowerCode.Contains("bcrypt") ||
                                             lowerCode.Contains("argon2") ||
                                             lowerCode.Contains("scrypt"));

            bool isVulnerable = containsPasswordField || insertsPasswordDirectly || updatesPasswordDirectly;

            return Task.FromResult(isVulnerable);
        }
    }
}
