// Copyright (c) InfoTeCS JSC. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Web.Mvc;

namespace WebUI
{
    /// <summary>
    ///     Фильтры.
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        ///     Регистрация фильтров.
        /// </summary>
        /// <param name="filters">Фильтры.</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
