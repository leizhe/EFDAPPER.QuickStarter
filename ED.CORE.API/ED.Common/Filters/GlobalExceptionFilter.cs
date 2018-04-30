using ED.Common.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ED.Common.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            var type = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            Log4NetHelper.WriteError(type, filterContext.Exception);
        }
    }
}
