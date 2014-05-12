using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HttpKit.Mvc.ActionResults
{
    public class AggregateResult : ActionResult
    {
        private readonly ActionResult[] delegates;

        public AggregateResult(params ActionResult[] delegates)
        {
            if (delegates == null) throw new ArgumentNullException("delegates");

            this.delegates = delegates;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            foreach (var @delegate in delegates)
            {
                @delegate.ExecuteResult(context);
            }
        }
    }
}
