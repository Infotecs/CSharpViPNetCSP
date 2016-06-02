// Copyright (c) InfoTeCS JSC. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
