using Microsoft.Owin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReverseProxy.Owin.Test
{
    [TestClass]
    public class DefaultLockManagerTest
    {
        [TestMethod]
        public void LockOnFreeKeyShouldReturnNull()
        {
            var key = new Mock<ICacheKey>();

            var sut = new DefaultLockManager();
            var result = sut.Lock(key.Object, TimeSpan.FromMinutes(10));

            Assert.IsNull(result);
        }

        [TestMethod]
        public void LockOnLockedKeyShouldReturnTask()
        {
            var key = new Mock<ICacheKey>();

            var sut = new DefaultLockManager();
            sut.Lock(key.Object, TimeSpan.FromMinutes(10));
            var result = sut.Lock(key.Object, TimeSpan.FromMinutes(10));

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UnlockOnLockedKeyShouldReleaseLock()
        {
            var key = new Mock<ICacheKey>();
            var response = new Mock<IOwinResponse>();

            var sut = new DefaultLockManager();
            sut.Lock(key.Object, TimeSpan.FromMinutes(10));
            sut.Unlock(key.Object, response.Object);
            var result = sut.Lock(key.Object, TimeSpan.FromMinutes(10));

            Assert.IsNull(result);
        }

        [TestMethod]
        public void TaskShouldResolveWhenUnlockingAnUnexpiredLock()
        {
            var key = new Mock<ICacheKey>();
            var response = new Mock<IOwinResponse>();

            var sut = new DefaultLockManager();
            sut.Lock(key.Object, TimeSpan.FromMinutes(10));
            var task = sut.Lock(key.Object, TimeSpan.FromMinutes(10));
            sut.Unlock(key.Object, response.Object);
            task.Wait();
        }

        [TestMethod]
        [ExpectedException(typeof(TimeoutException))]
        public void TaskShouldFailWhenLockExpires()
        {
            var key = new Mock<ICacheKey>();
            var response = new Mock<IOwinResponse>();

            var sut = new DefaultLockManager();
            sut.Lock(key.Object, TimeSpan.FromMinutes(10));
            var task = sut.Lock(key.Object, TimeSpan.FromMilliseconds(100));

            try
            {
                task.Wait();
            }
            catch (AggregateException e)
            {
                throw e.Flatten().GetBaseException();
            }
        }
    }
}
