using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using HttpKit.AspNet;

namespace HttpKit.Mvc.ActionResults
{
    public class IfModifiedSinceResult : ActionResult
    {
        private readonly Lazy<DateTime> lastModified;
        private readonly ActionResult ifModifiedSinceResult;

        public IfModifiedSinceResult(Lazy<DateTime> lastModified, ActionResult ifModifiedSinceResult)
        {
            if (lastModified == null) throw new ArgumentNullException("lastModified");
            if (ifModifiedSinceResult == null) throw new ArgumentNullException("ifModifiedSinceResult");

            this.lastModified = lastModified;
            this.ifModifiedSinceResult = ifModifiedSinceResult;
        }

        protected virtual bool IsModified(DateTime? ifModifiedSince)
        {
            return ifModifiedSince == null || lastModified.Value > ifModifiedSince;
        }

        protected virtual void ExecuteResultWhenModified(ControllerContext context)
        {
            context.HttpContext.Response.SetLastModified(lastModified.Value);
            ifModifiedSinceResult.ExecuteResult(context);
        }

        protected virtual void ExecuteResultWhenUnmodified(ControllerContext context)
        {
            context.HttpContext.Response.StatusCode = 304; // Not Modified
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            if (IsModified(context.HttpContext.Request.GetIfModifiedSince()))
            {
                ExecuteResultWhenModified(context);
            }
            else
            {
                ExecuteResultWhenUnmodified(context);
            }
        }
    }
}
