using HttpKit.AspNet;
using HttpKit.Caching;
using HttpKit.Ranges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HttpKit.Mvc.ActionResults
{
    public abstract class RangeResult : ActionResult
    {
        private string contentType;
        private IRangeUnit[] acceptedUnits;
        private Lazy<DateTime> lastModified;
        private Lazy<IEntityTag> entityTag;
        private EntityTagComparisonType entityTagComparison;

        public RangeResult(string contentType, IRangeUnit[] acceptedUnits, Lazy<DateTime> lastModified = null, Lazy<IEntityTag> entityTag = null, EntityTagComparisonType entityTagComparison = EntityTag.defaultComparisonType)
        {
            if (contentType == null) throw new ArgumentNullException("contentType");
            if (acceptedUnits == null) throw new ArgumentNullException("acceptedUnits");

            this.contentType = contentType;
            this.acceptedUnits = acceptedUnits;
            this.lastModified = lastModified;
            this.entityTag = entityTag;
            this.entityTagComparison = entityTagComparison;
        }

        public string ContentType
        {
            get { return contentType; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");

                contentType = value;
            }
        }

        public IRangeUnit[] AcceptedUnits
        {
            get { return acceptedUnits; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");

                acceptedUnits = value;
            }
        }

        protected void PrepareResponse(ControllerContext context, int status)
        {
            context.HttpContext.Response.StatusCode = status;
            context.HttpContext.Response.ContentType = contentType;
            context.HttpContext.Response.SetAcceptRange(new AcceptRange(acceptedUnits));
            if (lastModified != null)
            {
                context.HttpContext.Response.SetLastModified(lastModified.Value);
            }
            if (entityTag != null)
            {
                context.HttpContext.Response.SetETag(entityTag.Value);
            }
        }

        protected bool CanHandle(IIfRange ifRange)
        {
            switch (ifRange.Type)
            {
                case IfRangeType.LastModified:
                    return lastModified != null;

                case IfRangeType.EntityTag:
                    return entityTag != null;

                default:
                    return false;
            }
        }

        protected bool IsMatch(IIfRange ifRange)
        {
            switch (ifRange.Type)
            {
                case IfRangeType.LastModified:
                    return ifRange.LastModified >= lastModified.Value;

                case IfRangeType.EntityTag:
                    return ifRange.EntityTag.Equals(entityTag.Value, entityTagComparison);

                default:
                    return false;
            }
        }

        protected virtual void HandleNoRange(ControllerContext context)
        {
            PrepareResponse(context, 200); //Ok
            SendWholeEntity(context);
        }

        protected void HandleRange(ControllerContext context, IRange range)
        {
            if (range.Ranges.Length == 1)
            {
                TrySendRange(context, range.Unit, range.Ranges.First());
            }
            else
            {
                TrySendRanges(context, range.Unit, range.Ranges);
            }
        }

        protected virtual void TrySendRange(ControllerContext context, IRangeUnit unit, ISubRange range)
        {
            try
            {
                SendRange(context, unit, range);
            }
            catch (IndexOutOfRangeException)
            {
                SendRangeNotSatisfiable(context);
            }
        }

        protected virtual void TrySendRanges(ControllerContext context, IRangeUnit unit, ISubRange[] ranges)
        {
            throw new NotImplementedException();
        }

        protected virtual void SendRange(ControllerContext context, IRangeUnit unit, ISubRange range)
        {
            var rangeStream = GetRangeStream(context, unit, range);

            PrepareResponse(context, 206); //Partial Content
            context.HttpContext.Response.SetContentRange(
                new ContentRange(
                    unit,
                    new ContentSubRange(rangeStream.StartAt, rangeStream.EndAt),
                    new InstanceLength(rangeStream.TotalLength)
                )
            );
            rangeStream.CopyTo(context.HttpContext.Response.OutputStream);
        }

        protected virtual void SendRangeNotSatisfiable(ControllerContext context)
        {
            PrepareResponse(context, 416); //Requested range not satisfiable
        }

        protected abstract void SendWholeEntity(ControllerContext context);
        protected abstract RangeStreamDecorator GetRangeStream(ControllerContext context, IRangeUnit unit, ISubRange range);

        public override void ExecuteResult(ControllerContext context)
        {
            var range = context.HttpContext.Request.GetRange();
            var ifRange = context.HttpContext.Request.GetIfRange();

            if (range != null && (ifRange == null || !CanHandle(ifRange) || IsMatch(ifRange)))
            {
                HandleRange(context, range);
            }
            else
            {
                HandleNoRange(context);
            }
        }
    }
}
