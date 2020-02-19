using System.Web;
using System.Web.Mvc;
using QuoteManager.Filters;

namespace QuoteManager
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
