using HttpKit.Caching;
using HttpKit.Mvc.ActionResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HttpKit.Mvc
{
    public static class ValidationControllerExtensions
    {
        public static ActionResult IfMatch(this Controller controller, Lazy<IEntityTag> currentETag, ActionResult ifMatchResult)
        {
            return controller.IfMatch(currentETag, EntityTag.defaultComparisonType, ifMatchResult);
        }

        public static ActionResult IfMatch(this Controller controller, Lazy<IEntityTag> currentETag, EntityTagComparisonType comparisonType, ActionResult ifMatchResult)
        {
            if (currentETag == null) throw new ArgumentNullException("currentETag");
            if (ifMatchResult == null) throw new ArgumentNullException("ifMatchResult");

            return controller.IfMatch(currentETag, new[] { currentETag }.Select(et => et.Value), comparisonType, ifMatchResult);
        }

        public static ActionResult IfMatch(this Controller controller, Lazy<IEntityTag> currentETag, IEnumerable<IEntityTag> validETags, EntityTagComparisonType comparisonType, ActionResult ifMatchResult)
        {
            if (currentETag == null) throw new ArgumentNullException("currentETag");
            if (validETags == null) throw new ArgumentNullException("validETags");
            if (ifMatchResult == null) throw new ArgumentNullException("ifMatchResult");

            return new IfMatchResult(currentETag, condition => condition.IsValid(validETags, comparisonType), ifMatchResult);
        }

        public static ActionResult IfNoneMatch(this Controller controller, Lazy<IEntityTag> currentETag, ActionResult ifNoneMatchResult)
        {
            return controller.IfNoneMatch(currentETag, EntityTag.defaultComparisonType, ifNoneMatchResult);
        }

        public static ActionResult IfNoneMatch(this Controller controller, Lazy<IEntityTag> currentETag, EntityTagComparisonType comparisonType, ActionResult ifNoneMatchResult)
        {
            if (currentETag == null) throw new ArgumentNullException("currentETag");
            if (ifNoneMatchResult == null) throw new ArgumentNullException("ifNoneMatchResult");

            return controller.IfNoneMatch(currentETag, new[] { currentETag }.Select(et => et.Value), comparisonType, ifNoneMatchResult);
        }

        public static ActionResult IfNoneMatch(this Controller controller, Lazy<IEntityTag> currentETag, IEnumerable<IEntityTag> validETags, EntityTagComparisonType comparisonType, ActionResult ifNoneMatchResult)
        {
            if (currentETag == null) throw new ArgumentNullException("currentETag");
            if (validETags == null) throw new ArgumentNullException("validETags");
            if (ifNoneMatchResult == null) throw new ArgumentNullException("ifNoneMatchResult");

            return new IfNoneMatchResult(currentETag, condition => condition.IsValid(validETags, comparisonType), ifNoneMatchResult);
        }
    }
}
