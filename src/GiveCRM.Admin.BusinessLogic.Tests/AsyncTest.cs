using System;
using System.Threading;
using NUnit.Framework;

namespace GiveCRM.Admin.BusinessLogic.Tests
{
    public class AsyncTest
    {
        /// <summary>
        /// Wait for an async call to complete before returning control.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public static void WaitFor(Action<Action> action)
        {
            WaitFor(action, TimeSpan.FromSeconds(15));
        }
    
        /// <summary>
        /// Wait for an async call to complete before returning control.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        /// <param name="waitDuration">The duration to wait before failing.</param>
        public static void WaitFor(Action<Action> action, TimeSpan waitDuration)
        {
            var mre = new ManualResetEvent(false);
            Action callback = () => mre.Set();
            action(callback);
    
            if (!mre.WaitOne(waitDuration))
            {
                Assert.Fail("Timed out waiting for callback");
            }
        }
    }
}