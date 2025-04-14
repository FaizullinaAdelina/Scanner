using System.Threading.Tasks;

namespace Scanner.StaticChecks
{
    public class EvalExecCheck : StaticVulnerabilityCheck
    {
        public override string Name => "Использование eval/exec";

        public override Task<bool> CheckAsync(string code)
        {
            Logger.LogInfo("[EvalExecCheck] Начат поиск eval/exec...");

            bool found = code.Contains("eval") ||
                         code.Contains("exec") ||
                         code.Contains("EvaluateAsync") ||  // Поиск использования CSharpScript.EvaluateAsync
                         code.Contains("Process.Start");    // Поиск вызова exec'а через процессы

            if (found)
            {
                Logger.LogInfo("[EvalExecCheck] Найдена уязвимость: eval, exec, EvaluateAsync или Process.Start.");
            }
            else
            {
                Logger.LogInfo("[EvalExecCheck] Уязвимость не обнаружена.");
            }

            return Task.FromResult(found);
        }
    }
}
