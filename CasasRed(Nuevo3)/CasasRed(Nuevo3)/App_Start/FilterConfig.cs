using System.Web;
using System.Web.Mvc;

namespace CasasRed_Nuevo3_
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
