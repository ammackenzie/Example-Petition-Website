using System.Web;
using System.Web.Mvc;

namespace Final_Coursework___40091970
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
