using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading.Tasks;

namespace Scanner
{
    public class DOMBasedXSSCheck : VulnerabilityCheck
    {
        public override string Name => "DOM-based XSS";

        public override async Task<bool> CheckAsync(string url)
        {
            string payload = "<script>alert('DOMXSS')</script>";
            string testUrl = url.Contains("?")
                ? $"{url}&search={Uri.EscapeDataString(payload)}"
                : $"{url}?search={Uri.EscapeDataString(payload)}";

            var options = new ChromeOptions();
            options.AddArgument("--headless=new");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");

            var service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true; // 🔇 убираем окно cmd
            service.SuppressInitialDiagnosticInformation = true; // 🔇 убираем сообщение о запуске
            service.LogPath = "NUL"; // 🔇 убираем всю остальную болтовню
            service.EnableVerboseLogging = false; // 🔇 даже подробности не пишем
            using (var driver = new ChromeDriver(options))
            {
                try
                {
                    driver.Navigate().GoToUrl(testUrl);

                    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    var resultElement = wait.Until(d =>
                    {
                        try
                        {
                            var el = d.FindElement(By.Id("result"));
                            if (!string.IsNullOrEmpty(el.GetAttribute("innerHTML")))
                                return el;
                        }
                        catch (NoSuchElementException) { }
                        return null;
                    });

                    var innerHtml = resultElement?.GetAttribute("innerHTML") ?? string.Empty;

                    Console.WriteLine($"DOMBasedXSSCheck: innerHTML = {innerHtml}");

                    if (innerHtml.Contains(payload))
                    {
                        Console.WriteLine("❗ DOM-based XSS обнаружен!");
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[DOMBasedXSSCheck] Ошибка: {ex.Message}");
                }
            }

            return false;
        }
    }
}
