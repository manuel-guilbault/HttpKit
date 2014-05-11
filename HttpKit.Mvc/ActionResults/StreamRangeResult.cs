using HttpKit.Caching;
using HttpKit.Ranges;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HttpKit.Mvc.ActionResults
{
    public class StreamRangeResult : RangeResult, IDisposable
    {
        private static readonly IRangeUnit[] defaultAcceptedUnits = new[] { new RangeUnit("bytes") };

        private readonly Stream stream;

        private bool isDisposed = false;

        public StreamRangeResult(Stream stream, string contentType, IRangeUnit[] acceptedUnits = null, Lazy<DateTime> lastModified = null, Lazy<IEntityTag> entityTag = null, EntityTagComparisonType entityTagComparison = EntityTag.defaultComparisonType)
            : base(contentType, acceptedUnits ?? defaultAcceptedUnits, lastModified, entityTag, entityTagComparison)
        {
            if (stream == null) throw new ArgumentNullException("stream");

            this.stream = stream;
        }

        protected override void SendWholeEntity(ControllerContext context)
        {
            stream.CopyTo(context.HttpContext.Response.OutputStream);
        }

        protected override RangeStreamDecorator GetRangeStream(ControllerContext context, IRangeUnit unit, ISubRange range)
        {
            switch (range.Type)
            {
                case SubRangeType.OffsetFromStart:
                    return new RangeStreamDecorator(range.From, stream.Length - 1, stream);

                case SubRangeType.Closed:
                    return new RangeStreamDecorator(range.From, range.To, stream);

                case SubRangeType.OffsetFromEnd:
                    return new RangeStreamDecorator(Math.Max(0, stream.Length - range.From), stream.Length - 1, stream);

                default:
                    throw new InvalidProgramException("Unknown SubRangeType." + range.Type);
            }
        }

        public override void ExecuteResult(ControllerContext context)
        {
            try
            {
                base.ExecuteResult(context);
            }
            finally
            {
                Dispose();
            }
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                stream.Dispose();
                isDisposed = true;
            }
        }
    }
}
