using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web_Cafe.Common
{
    public class AuthorizationAdminAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session.Contents["isAdmin"] == null || !(bool)filterContext.HttpContext.Session.Contents["isAdmin"])
            {
                filterContext.Result = new RedirectToRouteResult(new 
                    RouteValueDictionary(new { controller = "AdminLogin", action = "Index", Area = "Admin" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}