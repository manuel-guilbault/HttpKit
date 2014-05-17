using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseProxy.Owin.Test
{
    public static class TaskExtensions
    {
        public static void AssertIsFaulted(this Task task)
        {
            try
            {
                task.Wait();
                Assert.Fail("Task SHOULD have failed");
            }
            catch (AggregateException e)
            {
            }
        }

        public static void AssertIsFaultedWith<TException>(this Task task)
        {
            try
            {
                task.Wait();
                Assert.Fail("Task SHOULD have failed with exception {0}", typeof(TException));
            }
            catch (AggregateException e)
            {
                if (!(e.InnerException is TException))
                {
                    Assert.Fail("Expected exception {0}, but exception {1} was thrown", typeof(TException), e.InnerException.GetType());
                }
            }
        }
    }
}
