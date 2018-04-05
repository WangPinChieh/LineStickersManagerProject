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
    public class SecurityAttribute : AuthorizeAttribute {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated == false)
                return false;
            else
                return true;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Account/Index");
        }
    }
}