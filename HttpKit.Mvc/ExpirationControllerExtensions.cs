using HttpKit.Mvc.ActionResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HttpKit.Mvc
{
    public static class ExpirationControllerExtensions
    {
        public static ActionResult IfModifiedSince(this Controller controller, Func<DateTime> lastModified, ActionResult ifModifiedSinceResult)
        {
            if (lastModified == null) throw new ArgumentNullException("lastModified");

            return controller.IfModifiedSince(new Lazy<DateTime>(lastModified), ifModifiedSinceResult);
        }

        public static ActionResult IfModifiedSince(this Controller controller, Lazy<DateTime> lastModified, ActionResult ifModifiedSinceResult)
        {
            if (lastModified == null) throw new ArgumentNullException("lastModified");
            if (ifModifiedSinceResult == null) throw new ArgumentNullException("ifModifiedSinceResult");

            return new IfModifiedSinceResult(lastModified, ifModifiedSinceResult);
        }

        public static ActionResult IfUnmodifiedSince(this Controller controller, Func<DateTime> lastModified, ActionResult ifUnmodifiedSinceResult)
        {
            if (lastModified == null) throw new ArgumentNullException("lastModified");

            return controller.IfUnmodifiedSince(new Lazy<DateTime>(lastModified), ifUnmodifiedSinceResult);
        }

        public static ActionResult IfUnmodifiedSince(this Controller controller, Lazy<DateTime> lastModified, ActionResult ifUnmodifiedSinceResult)
        {
            if (lastModified == null) throw new ArgumentNullException("lastModified");
            if (ifUnmodifiedSinceResult == null) throw new ArgumentNullException("ifUnmodifiedSinceResult");

            return new IfUnmodifiedSinceResult(lastModified, ifUnmodifiedSinceResult);
        }
    }
}
