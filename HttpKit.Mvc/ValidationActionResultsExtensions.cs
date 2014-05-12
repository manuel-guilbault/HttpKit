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
    public static class ValidationActionResultsExtensions
    {
        public static ActionResult IfMatch(this ActionResult result, Lazy<IEntityTag> currentETag)
        {
            return result.IfMatch(currentETag, EntityTag.defaultComparisonType);
        }

        public static ActionResult IfMatch(this ActionResult result, Lazy<IEntityTag> currentETag, EntityTagComparisonType comparisonType)
        {
            if (currentETag == null) throw new ArgumentNullException("currentETag");

            return result.IfMatch(currentETag, new[] { currentETag }.Select(et => et.Value), comparisonType);
        }

        public static ActionResult IfMatch(this ActionResult result, Lazy<IEntityTag> currentETag, IEnumerable<IEntityTag> validETags, EntityTagComparisonType comparisonType)
        {
            if (validETags == null) throw new ArgumentNullException("validETags");

            return new IfMatchResult(currentETag, condition => condition.IsValid(validETags, comparisonType), result);
        }

        public static ActionResult IfNoneMatch(this ActionResult result, Lazy<IEntityTag> currentETag)
        {
            return result.IfNoneMatch(currentETag, EntityTag.defaultComparisonType);
        }

        public static ActionResult IfNoneMatch(this ActionResult result, Lazy<IEntityTag> currentETag, EntityTagComparisonType comparisonType)
        {
            if (currentETag == null) throw new ArgumentNullException("currentETag");

            return result.IfNoneMatch(currentETag, new[] { currentETag }.Select(et => et.Value), comparisonType);
        }

        public static ActionResult IfNoneMatch(this ActionResult result, Lazy<IEntityTag> currentETag, IEnumerable<IEntityTag> validETags, EntityTagComparisonType comparisonType)
        {
            if (validETags == null) throw new ArgumentNullException("validETags");

            return new IfNoneMatchResult(currentETag, condition => condition.IsValid(validETags, comparisonType), result);
        }
    }
}
