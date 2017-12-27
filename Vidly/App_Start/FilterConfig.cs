using System.Web;
using System.Web.Mvc;

namespace Vidly
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // this filter redirects to user error page when an action throws an exception
            filters.Add(new HandleErrorAttribute());
            // Add Authurize filter globally to ensure whole websites need to access after user has been logged in.
            filters.Add(new AuthorizeAttribute());
            // The application and points will be longer available in HTTP channels.
            filters.Add(new RequireHttpsAttribute());
        }
    }
}
