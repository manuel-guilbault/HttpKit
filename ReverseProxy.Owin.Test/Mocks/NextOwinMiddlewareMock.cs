using Microsoft.Owin;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseProxy.Owin.Test
{
    public class NextOwinMiddlewareMock : Mock<OwinMiddleware>
    {
        public NextOwinMiddlewareMock()
            : base(MockBehavior.Default, null)
        {
            Setup(mw => mw.Invoke(It.IsAny<IOwinContext>())).Returns(Task.FromResult<object>(null)).Verifiable();
        }
    }
}
