using System;
using System.Threading.Tasks;

namespace Scanner.StaticChecks
{
    public class SqlInjectionConcatCheck : StaticVulnerabilityCheck
    {
        public override string Name => "SQL-инъекция через конкатенацию";

        public override async Task<bool> CheckAsync(string code)
        {
            return await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(code))
                    return false;

                string loweredCode = code.ToLower();

                // Признаки SQL-запросов
                bool hasSelect = loweredCode.Contains("select ");
                bool hasInsert = loweredCode.Contains("insert ");
                bool hasUpdate = loweredCode.Contains("update ");
                bool hasDelete = loweredCode.Contains("delete ");
                bool hasFrom = loweredCode.Contains("from ");
                bool hasWhere = loweredCode.Contains("where ");

                // Признаки небезопасной вставки данных
                bool usesPlusConcat = loweredCode.Contains("+");
                bool usesStringFormat = loweredCode.Contains("string.format");
                bool usesInterpolation = loweredCode.Contains("$\"");

                // Потенциальная SQL-инъекция
                bool isPotentialInjection =
                    (hasSelect || hasInsert || hasUpdate || hasDelete) &&
                    (usesPlusConcat || usesStringFormat || usesInterpolation);

                return isPotentialInjection;
            });
        }
    }
}
