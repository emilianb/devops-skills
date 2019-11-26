using System;
using System.Web.Mvc;
using System.Security.Principal;
using System.Web.Routing;

namespace Benday.Presidents.UnitTests.Presentation
{
    public static class ControllerSecurityUtility
    {

        public static void SetControllerContext(
            Controller controller,
            string username,
            string[] roles)
        {
            var httpContext = new MockHttpContext();

            httpContext.User = new GenericPrincipal(
                new GenericIdentity(username), roles);

            var controllerContext = new ControllerContext(
                new RequestContext(httpContext, new RouteData()), controller);

            controller.ControllerContext = controllerContext;
        }
    }
}
