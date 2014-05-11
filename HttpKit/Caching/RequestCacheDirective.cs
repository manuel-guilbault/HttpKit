using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpKit.Caching
{
    public class RequestCacheDirective : IRequestCacheDirective
	{
		public const string NO_CACHE = "no-cache";
		public const string NO_STORE = "no-store";
		public const string MAX_AGE = "max-age";
		public const string MAX_STALE = "max-stale";
		public const string MIN_FRESH = "min-fresh";
		public const string NO_TRANSFORM = "no-transform";
		public const string ONLY_IF_CACHED = "only-if-cached";

		public static readonly RequestCacheDirective NoCache = new RequestCacheDirective(NO_CACHE);
		public static readonly RequestCacheDirective NoStore = new RequestCacheDirective(NO_STORE);

		public static DeltaTimeRequestCacheDirective CreateMaxAge(TimeSpan delta)
		{
			return new DeltaTimeRequestCacheDirective(MAX_AGE, delta);
		}

		public static OptionalDeltaTimeRequestCacheDirective CreateMaxStale(TimeSpan? delta = null)
		{
			return new OptionalDeltaTimeRequestCacheDirective(MAX_STALE, delta);
		}

		public static DeltaTimeRequestCacheDirective CreateMinFresh(TimeSpan delta)
		{
			return new DeltaTimeRequestCacheDirective(MIN_FRESH, delta);
		}

		public static readonly RequestCacheDirective NoTransform = new RequestCacheDirective(NO_TRANSFORM);
		public static readonly RequestCacheDirective OnlyIfCached = new RequestCacheDirective(ONLY_IF_CACHED);

		public static RequestCacheDirectiveExtension CreateExtension(string name, string value = null)
		{
			if (name == null) throw new ArgumentNullException("name");
			if (name == "") throw new ArgumentException("name must not be empty", "name");

			return new RequestCacheDirectiveExtension(name, value);
		}

		protected RequestCacheDirective(string name)
		{
			this.Name = name;
		}

		public string Name { get; private set; }

		public override string ToString()
		{
			return Name;
		}
	}

	public class OptionalDeltaTimeRequestCacheDirective : RequestCacheDirective
	{
		protected internal OptionalDeltaTimeRequestCacheDirective(string name, TimeSpan? delta)
			: base(name)
		{
			this.Delta = delta;
		}

		public TimeSpan? Delta { get; private set; }

		public override string ToString()
		{
			return Delta == null
				? Name
				: string.Concat(Name, "=", (int)Math.Floor(Delta.Value.TotalSeconds));
		}
	}

	public class DeltaTimeRequestCacheDirective : RequestCacheDirective
	{
		protected internal DeltaTimeRequestCacheDirective(string name, TimeSpan delta)
			: base(name)
		{
			this.Delta = delta;
		}

		public TimeSpan Delta { get; private set; }

		public override string ToString()
		{
			return string.Concat(Name, "=", (int)Math.Floor(Delta.TotalSeconds));
		}
	}

	public class RequestCacheDirectiveExtension : RequestCacheDirective
	{
		protected internal RequestCacheDirectiveExtension(string name, string value = null)
			: base(name)
		{
			this.Value = value;
		}

		public string Value { get; private set; }

		public override string ToString()
		{
			return Value == null
				? Name
				: string.Concat(Name, "=", Value);
		}
	}
}
