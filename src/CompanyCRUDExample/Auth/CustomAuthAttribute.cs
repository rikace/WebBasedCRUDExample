using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using TechStudioTest.Auth;

namespace TechStudioTest.Auth
{
    public class CustomAuthAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var currentUser = httpContext.Session["TechStudioTestAuth"];
            if (currentUser == null)
            {
                // The session has expired.
                return false;
            }
            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            RedirectToRouteResult routeData = null;

            var currentUser = filterContext.HttpContext.Session["TechStudioTechAuth"];
            if (currentUser == null)
            {
                routeData = new RedirectToRouteResult
                    (new System.Web.Routing.RouteValueDictionary
                    (new
                    {
                        controller = "Account",
                        action = "Login",
                        ReturnUrl = filterContext.HttpContext.Request.RawUrl
                    }
                    ));
            }
            else
            {
                routeData = new RedirectToRouteResult
                    (new System.Web.Routing.RouteValueDictionary
                    (new
                    {
                        controller = "Error",
                        action = "AccessDenied"
                    }
                    ));
            }

            filterContext.Result = routeData;
        }
    }
}