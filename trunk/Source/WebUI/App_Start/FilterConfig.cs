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
