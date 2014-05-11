using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using HttpKit.AspNet;

namespace HttpKit.Mvc.ActionResults
{
    public class IfUnmodifiedSinceResult : ActionResult
    {
        private readonly Lazy<DateTime> lastModified;
        private readonly ActionResult ifUnmodifiedSinceResult;

        public IfUnmodifiedSinceResult(Lazy<DateTime> lastModified, ActionResult ifUnmodifiedSinceResult)
        {
            if (lastModified == null) throw new ArgumentNullException("lastModified");
            if (ifUnmodifiedSinceResult == null) throw new ArgumentNullException("ifUnmodifiedSinceResult");

            this.lastModified = lastModified;
            this.ifUnmodifiedSinceResult = ifUnmodifiedSinceResult;
        }

        protected virtual bool IsModified(DateTime? ifUnmodifiedSince)
        {
            return ifUnmodifiedSince == null || lastModified.Value > ifUnmodifiedSince;
        }

        protected virtual void ExecuteResultWhenUnmodified(ControllerContext context)
        {
            ifUnmodifiedSinceResult.ExecuteResult(context);
        }

        protected virtual void ExecuteResultWhenModified(ControllerContext context)
        {
            context.HttpContext.Response.StatusCode = 412; // Precondition Failed
            context.HttpContext.Response.SetLastModified(lastModified.Value);
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            if (!IsModified(context.HttpContext.Request.GetIfUnmodifiedSince()))
            {
                ExecuteResultWhenUnmodified(context);
            }
            else
            {
                ExecuteResultWhenModified(context);
            }
        }
    }
}
