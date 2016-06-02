// Copyright (c) InfoTeCS JSC. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Web.Mvc;
using System.Web.Routing;

namespace WebUI
{
    /// <summary>
    ///     Маршруты.
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        ///     Регистрация маршрутов.
        /// </summary>
        /// <param name="routes">Маршруты.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Cryptography", action = "Hash", id = UrlParameter.Optional }
                );
        }
    }
}
