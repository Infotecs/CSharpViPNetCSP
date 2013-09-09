using System;
using System.ServiceModel.Web;
using NLog;

namespace Infotecs.Shellma
{
    /// <summary>
    ///     Program.
    /// </summary>
    internal class Program
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();
        private static WebServiceHost host;

        /// <summary>
        ///     Точка входа.
        /// </summary>
        private static void Main()
        {
            log.Debug("Starting...");
            host = new WebServiceHost(typeof(ServiceProvider));
            host.Open();
            log.Info("Shellma is running. Press any key to stop.");
            Console.ReadKey();
            host.Close();
        }
    }
}
