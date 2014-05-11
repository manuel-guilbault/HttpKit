using HttpKit.AspNet;
using HttpKit.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HttpKit.Mvc.ActionResults
{
    public class IfNoneMatchResult : ActionResult
    {
        private readonly Lazy<IEntityTag> currentETag;
        private readonly Func<IEntityTagCondition, bool> etagValidator;
        private readonly ActionResult ifNoneMatchResult;

        public IfNoneMatchResult(Lazy<IEntityTag> currentETag, ActionResult ifNoneMatchResult)
            : this(currentETag, EntityTag.defaultComparisonType, ifNoneMatchResult)
        {
        }

        public IfNoneMatchResult(Lazy<IEntityTag> currentETag, EntityTagComparisonType comparisonType, ActionResult ifNoneMatchResult)
            : this(currentETag, condition => condition.IsValid(currentETag.Value, comparisonType), ifNoneMatchResult)
        {
        }

        public IfNoneMatchResult(Lazy<IEntityTag> currentETag, Func<IEntityTagCondition, bool> etagValidator, ActionResult ifNoneMatchResult)
        {
            if (currentETag == null) throw new ArgumentNullException("currentETag");
            if (etagValidator == null) throw new ArgumentNullException("etagValidator");
            if (ifNoneMatchResult == null) throw new ArgumentNullException("ifNoneMatchResult");

            this.currentETag = currentETag;
            this.etagValidator = etagValidator;
            this.ifNoneMatchResult = ifNoneMatchResult;
        }

        protected virtual bool IsMatch(IEntityTagCondition condition)
        {
            return condition == null || !etagValidator(condition);
        }

        protected virtual void ExecuteResultWhenNoMatch(ControllerContext context)
        {
            context.HttpContext.Response.SetETag(currentETag.Value);
            ifNoneMatchResult.ExecuteResult(context);
        }

        protected virtual void ExecuteResultWhenMatch(ControllerContext context)
        {
            context.HttpContext.Response.StatusCode = 412; // Precondition Failed
            context.HttpContext.Response.SetETag(currentETag.Value);
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            if (!IsMatch(context.HttpContext.Request.GetIfNoneMatch()))
            {
                ExecuteResultWhenNoMatch(context);
            }
            else
            {
                ExecuteResultWhenMatch(context);
            }
        }
    }
}
