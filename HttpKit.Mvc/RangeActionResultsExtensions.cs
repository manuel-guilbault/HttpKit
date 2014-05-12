using HttpKit.Caching;
using HttpKit.Mvc.ActionResults;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HttpKit.Mvc
{
    public static class RangeActionResultsExtensions
    {
        public static ActionResult Range(this ActionResult result, Stream stream, string contentType)
        {
            return result.Merge(new StreamRangeResult(stream, contentType));
        }

        public static ActionResult Range(this ActionResult result, Stream stream, string contentType, DateTime lastModified)
        {
            return result.Merge(new StreamRangeResult(stream, contentType, lastModified: new Lazy<DateTime>(() => lastModified)));
        }

        public static ActionResult Range(this ActionResult result, Stream stream, string contentType, Lazy<DateTime> lastModified)
        {
            return result.Merge(new StreamRangeResult(stream, contentType, lastModified: lastModified));
        }

        public static ActionResult Range(this ActionResult result, Stream stream, string contentType, IEntityTag entityTag)
        {
            return result.Merge(new StreamRangeResult(stream, contentType, entityTag: new Lazy<IEntityTag>(() => entityTag)));
        }

        public static ActionResult Range(this ActionResult result, Stream stream, string contentType, Lazy<IEntityTag> entityTag)
        {
            return result.Merge(new StreamRangeResult(stream, contentType, entityTag: entityTag));
        }

        public static ActionResult Range(this ActionResult result, Stream stream, string contentType, IEntityTag entityTag, EntityTagComparisonType entityTagComparison)
        {
            return result.Merge(new StreamRangeResult(stream, contentType, entityTag: new Lazy<IEntityTag>(() => entityTag), entityTagComparison: entityTagComparison));
        }

        public static ActionResult Range(this ActionResult result, Stream stream, string contentType, Lazy<IEntityTag> entityTag, EntityTagComparisonType entityTagComparison)
        {
            return result.Merge(new StreamRangeResult(stream, contentType, entityTag: entityTag, entityTagComparison: entityTagComparison));
        }

        public static ActionResult Range(this ActionResult result, Stream stream, string contentType, DateTime lastModified, IEntityTag entityTag)
        {
            return result.Merge(new StreamRangeResult(stream, contentType, lastModified: new Lazy<DateTime>(() => lastModified), entityTag: new Lazy<IEntityTag>(() => entityTag)));
        }

        public static ActionResult Range(this ActionResult result, Stream stream, string contentType, Lazy<DateTime> lastModified, Lazy<IEntityTag> entityTag)
        {
            return result.Merge(new StreamRangeResult(stream, contentType, lastModified: lastModified, entityTag: entityTag));
        }

        public static ActionResult Range(this ActionResult result, Stream stream, string contentType, DateTime lastModified, IEntityTag entityTag, EntityTagComparisonType entityTagComparison)
        {
            return result.Merge(new StreamRangeResult(stream, contentType, lastModified: new Lazy<DateTime>(() => lastModified), entityTag: new Lazy<IEntityTag>(() => entityTag), entityTagComparison: entityTagComparison));
        }

        public static ActionResult Range(this ActionResult result, Stream stream, string contentType, Lazy<DateTime> lastModified, Lazy<IEntityTag> entityTag, EntityTagComparisonType entityTagComparison)
        {
            return result.Merge(new StreamRangeResult(stream, contentType, lastModified: lastModified, entityTag: entityTag, entityTagComparison: entityTagComparison));
        }
    }
}
