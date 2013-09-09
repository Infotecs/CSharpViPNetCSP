using System;
using System.ServiceModel;
using Infotecs.Shellma.Cors;
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
        /// </summary>
        private static void Main()
        {
            log.Debug("Starting...");
            host = new CorsEnabledServiceHost(
                typeof(ServiceProvider), new[] { new Uri("http://localhost:5030/shellma") });
            host.Open();
            log.Info("Shellma is running. Press any key to stop.");
            Console.ReadKey();
            host.Close();
        }
    }
}
