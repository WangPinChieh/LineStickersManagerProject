using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LineStickersManager
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new SecurityAttribute());
        }
    }
    public class SecurityAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.Controller is Controllers.AccountController)
                return;
            else
            {
                if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                    filterContext.Result = new RedirectResult("/Account/Index");
            }
        }
    }
}