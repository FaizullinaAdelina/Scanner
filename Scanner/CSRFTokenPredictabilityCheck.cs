using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Scanner
{
    public class CSRFTokenPredictabilityCheck : VulnerabilityCheck
    {
        // Регулярное выражение для извлечения CSRF-токена из HTML-формы
        private static readonly Regex tokenRegex = new Regex(
            @"<input\s+[^>]*name\s*=\s*[""']csrf_token[""'][^>]*value\s*=\s*[""']([^""']+)[""']",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public override string Name => "Cross-Site Request Forgery (CSRF) - Predictable Token";

        public override async Task<bool> CheckAsync(string url)
        {
            // Используем Selenium WebDriver для эмуляции браузера
            var options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");

            string token1 = null, token2 = null;

            using (var driver = new ChromeDriver(options))
            {
                try
                {
                    // Переходим на страницу с формой
                    string targetUrl = url.EndsWith("/") ? url + "update-email" : url + "/update-email";
                    driver.Navigate().GoToUrl(targetUrl);

                    // Ждем загрузки страницы
                    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    wait.Until(d => d.FindElement(By.TagName("form")));

                    string pageSource1 = driver.PageSource;
                    var match1 = tokenRegex.Match(pageSource1);
                    if (match1.Success)
                        token1 = match1.Groups[1].Value;

                    // Обновляем страницу, чтобы получить токен снова
                    driver.Navigate().Refresh();
                    wait.Until(d => d.FindElement(By.TagName("form")));
                    string pageSource2 = driver.PageSource;
                    var match2 = tokenRegex.Match(pageSource2);
                    if (match2.Success)
                        token2 = match2.Groups[1].Value;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"CSRFTokenPredictabilityCheck error: {ex.Message}");
                }
                finally
                {
                    driver.Quit();
                }
            }

            Console.WriteLine($"Token1: {token1}");
            Console.WriteLine($"Token2: {token2}");

            // Если токены получены и они одинаковые, значит токен предсказуемый
            if (!string.IsNullOrEmpty(token1) && token1 == token2)
            {
                return true;
            }
            return false;
        }
    }
}