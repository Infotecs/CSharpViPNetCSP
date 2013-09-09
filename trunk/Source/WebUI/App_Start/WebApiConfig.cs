using System.Web.Http;

namespace WebUI
{
    /// <summary>
    ///     WebApi.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        ///     Регистрация.
        /// </summary>
        /// <param name="config">Конфигурация.</param>
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );
        }
    }
}
