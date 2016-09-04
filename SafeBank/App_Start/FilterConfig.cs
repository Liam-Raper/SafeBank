using System.Web.Mvc;
using SafeBank.Filters;

namespace SafeBank
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new UserActivityFilter());
            filters.Add(new HandleErrorAttribute());
        }
    }
}