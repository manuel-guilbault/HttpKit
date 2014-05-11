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
    public class IfMatchResult : ActionResult
    {
        private readonly Lazy<IEntityTag> currentETag;
        private readonly Func<IEntityTagCondition, bool> etagValidator;
        private readonly ActionResult ifMatchResult;

        public IfMatchResult(Lazy<IEntityTag> currentETag, ActionResult ifMatchResult)
            : this(currentETag, EntityTag.defaultComparisonType, ifMatchResult)
        {
        }

        public IfMatchResult(Lazy<IEntityTag> currentETag, EntityTagComparisonType comparisonType, ActionResult ifMatchResult)
            : this(currentETag, condition => condition.IsValid(currentETag.Value, comparisonType), ifMatchResult)
        {
        }

        public IfMatchResult(Lazy<IEntityTag> currentETag, Func<IEntityTagCondition, bool> etagValidator, ActionResult ifMatchResult)
        {
            if (currentETag == null) throw new ArgumentNullException("currentETag");
            if (etagValidator == null) throw new ArgumentNullException("etagValidator");
            if (ifMatchResult == null) throw new ArgumentNullException("ifMatchResult");

            this.currentETag = currentETag;
            this.etagValidator = etagValidator;
            this.ifMatchResult = ifMatchResult;
        }

        protected virtual bool IsMatch(IEntityTagCondition condition)
        {
            return condition == null || etagValidator(condition);
        }

        protected virtual void ExecuteResultWhenMatch(ControllerContext context)
        {
            context.HttpContext.Response.SetETag(currentETag.Value);
            ifMatchResult.ExecuteResult(context);
        }

        protected virtual void ExecuteResultWhenNoMatch(ControllerContext context)
        {
            context.HttpContext.Response.StatusCode = 412; // Precondition Failed
            context.HttpContext.Response.SetETag(currentETag.Value);
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            if (IsMatch(context.HttpContext.Request.GetIfMatch()))
            {
                ExecuteResultWhenMatch(context);
            }
            else
            {
                ExecuteResultWhenNoMatch(context);
            }
        }
    }
}
