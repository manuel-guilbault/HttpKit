using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpKit.Caching
{
    public class EntityTagCondition : IEntityTagCondition
    {
        public static readonly EntityTagCondition Any = new AnyEntityTagCondition();

        private readonly IEntityTag[] validTags;

        protected EntityTagCondition()
        {
            validTags = new IEntityTag[0];
        }

        public EntityTagCondition(params IEntityTag[] validTags)
        {
            if (validTags == null) throw new ArgumentNullException("validTags");
            if (!validTags.Any()) throw new ArgumentException("validTags must not be empty", "validTags");
            if (validTags.Any(tag => tag == null)) throw new ArgumentException("validTags must not contain null values", "validTags");

            this.validTags = validTags.ToArray();
        }

        public IEnumerable<IEntityTag> ValidTags
        {
            get { return validTags; }
        }

        public bool IsValid(IEntityTag entityTag)
        {
            return IsValid(entityTag, EntityTag.defaultComparisonType);
        }

        public bool IsValid(IEntityTag entityTag, EntityTagComparisonType comparisonType)
        {
            if (entityTag == null) throw new ArgumentNullException("entityTag");

            return IsValid(new[] { entityTag }, comparisonType);
        }

        public bool IsValid(IEnumerable<IEntityTag> entityTags)
        {
            return IsValid(entityTags, EntityTag.defaultComparisonType);
        }

        public virtual bool IsValid(IEnumerable<IEntityTag> entityTags, EntityTagComparisonType comparisonType)
        {
            if (entityTags == null) throw new ArgumentNullException("entityTags");

            return validTags.Intersect(entityTags, new EntityTagEqualityComparer(comparisonType)).Any();
        }

        public override string ToString()
        {
            return string.Join(",", ValidTags);
        }

        private class AnyEntityTagCondition : EntityTagCondition
        {
            public override bool IsValid(IEnumerable<IEntityTag> entityTags, EntityTagComparisonType comparisonType)
            {
                return entityTags.Any();
            }

            public override string ToString()
            {
                return "*";
            }
        }
    }
}
