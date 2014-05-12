using HttpKit.Mvc.ActionResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HttpKit.Mvc
{
    public static class ExpirationActionResultsExtensions
    {
        public static ActionResult IfModifiedSince(this ActionResult result, Func<DateTime> lastModified)
        {
            if (lastModified == null) throw new ArgumentNullException("lastModified");

            return result.IfModifiedSince(new Lazy<DateTime>(lastModified));
        }

        public static ActionResult IfModifiedSince(this ActionResult result, Lazy<DateTime> lastModified)
        {
            return new IfModifiedSinceResult(lastModified, result);
        }

        public static ActionResult IfUnmodifiedSince(this ActionResult result, Func<DateTime> lastModified)
        {
            if (lastModified == null) throw new ArgumentNullException("lastModified");

            return result.IfUnmodifiedSince(new Lazy<DateTime>(lastModified));
        }

        public static ActionResult IfUnmodifiedSince(this ActionResult result, Lazy<DateTime> lastModified)
        {
            return new IfUnmodifiedSinceResult(lastModified, result);
        }
    }
}
