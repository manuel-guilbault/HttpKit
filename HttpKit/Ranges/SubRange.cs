using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpKit.Ranges
{
    public class SubRange : ISubRange
	{
		public static ISubRange CreateOffsetFromStart(long offset)
		{
			if (offset < 0) throw new ArgumentException("offset must be equal to or greater than zero", "offset");

			return new SubRange(SubRangeType.OffsetFromStart, offset, 0);
		}

		public static ISubRange CreateClosedRange(long from, long to)
		{
			if (from < 0) throw new ArgumentException("from must be equal to or greater than zero", "from");
			if (to < from) throw new ArgumentException("to must be equal to or greater than from", "to");

			return new SubRange(SubRangeType.Closed, from, to);
		}

		public static ISubRange CreateOffsetFromEnd(long offset)
		{
			if (offset <= 0) throw new ArgumentException("offset must be greater than zero", "offset");

			return new SubRange(SubRangeType.OffsetFromEnd, offset, 0);
		}

		private SubRange(SubRangeType type, long from, long to)
		{
			this.Type = type;
			this.From = from;
			this.To = to;
		}

		public SubRangeType Type { get; private set; }
		public long From { get; private set; }
		public long To { get; private set; }

		public override string ToString()
		{
			switch (Type)
			{
				case SubRangeType.OffsetFromStart:
					return From + "-";

				case SubRangeType.Closed:
					return string.Concat(From, "-", To);

				case SubRangeType.OffsetFromEnd:
					return "-" + From;

				default:
					throw new InvalidProgramException("Unknown SubRangeType." + Type);
			}
		}

        public override int GetHashCode()
        {
            switch (Type)
            {
                case SubRangeType.OffsetFromStart:
                    return unchecked((int)From);

                case SubRangeType.Closed:
                    return unchecked((int)From * 17 + (int)To);

                case SubRangeType.OffsetFromEnd:
                    return unchecked((int)To * 23);

                default:
                    throw new InvalidProgramException("Unknown SubRangeType." + Type);
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ISubRange);
        }

        public virtual bool Equals(ISubRange other)
        {
            if (other == null || Type != other.Type)
            {
                return false;
            }

            switch (Type)
            {
                case SubRangeType.OffsetFromStart:
                    return From == other.From;

                case SubRangeType.Closed:
                    return From == other.From && To == other.To;

                case SubRangeType.OffsetFromEnd:
                    return To == other.To;

                default:
                    throw new InvalidProgramException("Unknown SubRangeType." + Type);
            }
        }
	}
}
