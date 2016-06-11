using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Security.Interfaces.User;

namespace SafeBank.Filters
{
    public class UserActivityFilter : IActionFilter
    {

        private IUserActivities _userActivitiesobj;

        private IUserActivities UserActivities => _userActivitiesobj ?? (_userActivitiesobj = DependencyResolver.Current.GetService<IUserActivities>());
        
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated) return;
            var username = filterContext.HttpContext.User.Identity.Name;
            UserActivities.UpdateLastActionDateTime(username);
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}