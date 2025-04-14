using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Scanner
{
    public class InvalidSslCertificateCheck : VulnerabilityCheck
    {
        public override string Name => "Invalid SSL Certificate";

        public override async Task<bool> CheckAsync(string url)
        {
            try
            {
                var uri = new Uri(url);
                using (var tcpClient = new TcpClient())
                {
                    await tcpClient.ConnectAsync(uri.Host, 443);

                    using (var sslStream = new SslStream(tcpClient.GetStream(), false, new RemoteCertificateValidationCallback(ValidateServerCertificate), null))
                    {
                        try
                        {
                            await sslStream.AuthenticateAsClientAsync(uri.Host);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка при установке SSL-соединения: {ex.Message}");
                            return true;
                        }

                        var cert = new X509Certificate2(sslStream.RemoteCertificate);

                        if (!cert.Verify())
                        {
                            Console.WriteLine($"Сертификат не прошёл проверку доверия системы.");
                            return true;
                        }

                        if (DateTime.Now < cert.NotBefore || DateTime.Now > cert.NotAfter)
                        {
                            Console.WriteLine($"Срок действия сертификата истёк или ещё не начался.");
                            return true;
                        }

                        if (IsSelfSigned(cert))
                        {
                            Console.WriteLine($"Используется самоподписанный сертификат.");
                            return true;
                        }

                        Console.WriteLine($"SSL-сертификат действителен.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при проверке SSL-сертификата: {ex.Message}");
                return true;
            }
        }

        private bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            // Проверка политики. Если нет ошибок, значит всё ок.
            return sslPolicyErrors == SslPolicyErrors.None;
        }

        private bool IsSelfSigned(X509Certificate2 cert)
        {
            return cert.Subject == cert.Issuer;
        }
    }
}
