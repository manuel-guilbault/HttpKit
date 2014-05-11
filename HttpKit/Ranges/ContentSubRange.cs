using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpKit.Ranges
{
    public class ContentSubRange : IContentSubRange
    {
        public static readonly IContentSubRange Unknown = new UnknownContentSubRange();

        private readonly long startAt;
        private readonly long endAt;

        private ContentSubRange()
        {
        }

        public ContentSubRange(long startAt, long endAt)
        {
            if (startAt < 0) throw new ArgumentException("startAt must be equal to or greater than zero", "startAt");
            if (endAt < startAt) throw new ArgumentException("endAt must be equal to or greater than startAt", "endAt");

            this.startAt = startAt;
            this.endAt = endAt;
        }

        public long StartAt
        {
            get { return startAt; }
        }

        public long EndAt
        {
            get { return endAt; }
        }

        public override string ToString()
        {
            return string.Concat(startAt, "-", endAt);
        }

        public override int GetHashCode()
        {
            return unchecked((int)startAt * 17 + (int)endAt);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as IContentSubRange);
        }

        public virtual bool Equals(IContentSubRange other)
        {
            return other != null && StartAt == other.StartAt && EndAt == other.EndAt;
        }

        private class UnknownContentSubRange : ContentSubRange
        {
            public override string ToString()
            {
                return "*";
            }

            public override int GetHashCode()
            {
                return 1;
            }

            public override bool Equals(IContentSubRange other)
            {
                return other != null && ReferenceEquals(other, this);
            }
        }
    }
}
