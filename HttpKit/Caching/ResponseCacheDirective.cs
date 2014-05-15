using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpKit.Caching
{
    public class ResponseCacheDirective : IResponseCacheDirective
	{
		public const string PUBLIC = "public";
		public const string PRIVATE = "private";
		public const string NO_CACHE = "no-cache";
		public const string NO_STORE = "no-store";
		public const string NO_TRANSFORM = "no-transform";
		public const string MUST_REVALIDATE = "must-revalidate";
		public const string PROXY_REVALIDATE = "proxy-revalidate";
		public const string MAX_AGE = "max-age";
		public const string SHARED_MAX_AGE = "s-maxage";

		public static readonly ResponseCacheDirective Public = new ResponseCacheDirective(PUBLIC);
        public static readonly ResponseCacheDirective Private = CreatePrivate();

		public static FieldListResponseCacheDirective CreatePrivate(params string[] fields)
		{
			return new FieldListResponseCacheDirective(PRIVATE, fields);
		}

        public static ResponseCacheDirective NoCache = CreateNoCache();

		public static FieldListResponseCacheDirective CreateNoCache(params string[] fields)
		{
			return new FieldListResponseCacheDirective(NO_CACHE, fields);
		}

		public static readonly ResponseCacheDirective NoStore = new ResponseCacheDirective(NO_STORE);
		public static readonly ResponseCacheDirective NoTransform = new ResponseCacheDirective(NO_TRANSFORM);
		public static readonly ResponseCacheDirective MustRevalidate = new ResponseCacheDirective(MUST_REVALIDATE);
		public static readonly ResponseCacheDirective ProxyRevalidate = new ResponseCacheDirective(PROXY_REVALIDATE);

		public static DeltaTimeResponseCacheDirective CreateMaxAge(TimeSpan delta)
		{
			return new DeltaTimeResponseCacheDirective(MAX_AGE, delta);
		}

		public static DeltaTimeResponseCacheDirective CreateSharedMaxAge(TimeSpan delta)
		{
			return new DeltaTimeResponseCacheDirective(SHARED_MAX_AGE, delta);
		}

		public static ResponseCacheDirectiveExtension CreateExtension(string name, string value = null)
		{
			if (name == null) throw new ArgumentNullException("name");
			if (name == "") throw new ArgumentException("name must not be empty", "name");

			return new ResponseCacheDirectiveExtension(name, value);
		}

		protected ResponseCacheDirective(string name)
		{
			this.Name = name;
		}

		public string Name { get; private set; }

		public override string ToString()
		{
			return Name;
		}
	}

	public class FieldListResponseCacheDirective : ResponseCacheDirective
	{
		protected internal FieldListResponseCacheDirective(string name, params string[] fields)
			: base(name)
		{
			if (fields == null) throw new ArgumentNullException("fields");

			this.Fields = fields.ToArray();
		}

		public string[] Fields { get; private set; }

		public override string ToString()
		{
			return Fields.Any()
				? string.Concat(Name, "=\"", string.Join(",", Fields), "\"")
				: Name;
		}
	}

	public class DeltaTimeResponseCacheDirective : ResponseCacheDirective
	{
		protected internal DeltaTimeResponseCacheDirective(string name, TimeSpan delta)
			: base(name)
		{
			this.Delta = delta;
		}

		public TimeSpan Delta { get; private set; }

		public override string ToString()
		{
			return string.Concat(Name, "=", Delta.TotalSeconds);
		}
	}

	public class ResponseCacheDirectiveExtension : ResponseCacheDirective
	{
		protected internal ResponseCacheDirectiveExtension(string name, string value = null)
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
