using System;
using System.ServiceModel;
using Infotecs.Shellma.Cors;
using Infotecs.Shellma.Properties;
using NLog;

namespace Infotecs.Shellma
{
    /// <summary>
    ///     Program.
    /// </summary>
    internal class Program
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();
        private static ServiceHost host;

        /// <summary>
        ///     Точка входа.
        /// для запуска без прав администратора нужно зарезервировать адрес для пользователя:
        /// netsh http add urlacl url=http://+:5030/shellma user="[localuser]"
        /// netsh http delete urlacl url=http://+:5030/shellma
        /// </summary>
        private static void Main()
        {
            var serviceAddress = Settings.Default.ServiceAddress;
            log.Debug("Starting...");

            host = new CorsEnabledServiceHost(
                typeof(ServiceProvider), new[] { new Uri(serviceAddress) });

            host.Open();
            log.Info("Shellma is running on: {0}. Press any key to stop.", serviceAddress);

            Console.ReadKey();
            host.Close();
        }
    }
}
