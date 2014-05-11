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
    public static class RangeControllerExtensions
    {
        public static StreamRangeResult Range(this Controller controller, Stream stream, string contentType)
        {
            return new StreamRangeResult(stream, contentType);
        }

        public static StreamRangeResult Range(this Controller controller, Stream stream, string contentType, DateTime lastModified)
        {
            return new StreamRangeResult(stream, contentType, lastModified: new Lazy<DateTime>(() => lastModified));
        }

        public static StreamRangeResult Range(this Controller controller, Stream stream, string contentType, Lazy<DateTime> lastModified)
        {
            return new StreamRangeResult(stream, contentType, lastModified: lastModified);
        }

        public static StreamRangeResult Range(this Controller controller, Stream stream, string contentType, IEntityTag entityTag)
        {
            return new StreamRangeResult(stream, contentType, entityTag: new Lazy<IEntityTag>(() => entityTag));
        }

        public static StreamRangeResult Range(this Controller controller, Stream stream, string contentType, Lazy<IEntityTag> entityTag)
        {
            return new StreamRangeResult(stream, contentType, entityTag: entityTag);
        }

        public static StreamRangeResult Range(this Controller controller, Stream stream, string contentType, IEntityTag entityTag, EntityTagComparisonType entityTagComparison)
        {
            return new StreamRangeResult(stream, contentType, entityTag: new Lazy<IEntityTag>(() => entityTag), entityTagComparison: entityTagComparison);
        }

        public static StreamRangeResult Range(this Controller controller, Stream stream, string contentType, Lazy<IEntityTag> entityTag, EntityTagComparisonType entityTagComparison)
        {
            return new StreamRangeResult(stream, contentType, entityTag: entityTag, entityTagComparison: entityTagComparison);
        }

        public static StreamRangeResult Range(this Controller controller, Stream stream, string contentType, DateTime lastModified, IEntityTag entityTag)
        {
            return new StreamRangeResult(stream, contentType, lastModified: new Lazy<DateTime>(() => lastModified), entityTag: new Lazy<IEntityTag>(() => entityTag));
        }

        public static StreamRangeResult Range(this Controller controller, Stream stream, string contentType, Lazy<DateTime> lastModified, Lazy<IEntityTag> entityTag)
        {
            return new StreamRangeResult(stream, contentType, lastModified: lastModified, entityTag: entityTag);
        }

        public static StreamRangeResult Range(this Controller controller, Stream stream, string contentType, DateTime lastModified, IEntityTag entityTag, EntityTagComparisonType entityTagComparison)
        {
            return new StreamRangeResult(stream, contentType, lastModified: new Lazy<DateTime>(() => lastModified), entityTag: new Lazy<IEntityTag>(() => entityTag), entityTagComparison: entityTagComparison);
        }

        public static StreamRangeResult Range(this Controller controller, Stream stream, string contentType, Lazy<DateTime> lastModified, Lazy<IEntityTag> entityTag, EntityTagComparisonType entityTagComparison)
        {
            return new StreamRangeResult(stream, contentType, lastModified: lastModified, entityTag: entityTag, entityTagComparison: entityTagComparison);
        }
    }
}
