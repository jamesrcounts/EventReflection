using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventReflection.Demo
{
    using System;

    [TestClass]
    public class SimpleEventDemo
    {
        [TestMethod]
        public void GetPocoEventInvocationList()
        {
            Poco poco = new Poco();
            poco.ProcessCompleted += Domain.HandleProcessCompleted;

            EventHandler pocoDelegate = poco.GetProcessCompletedHandler();
            DelegateUtility.VerifyInvocationList(pocoDelegate);
        }

        [TestMethod]
        public void RaiseCompletedEventWhenProcessCompletes()
        {
            bool raisedEvent = false;
            Poco poco = new Poco();
            poco.ProcessCompleted += (s, e) => raisedEvent = true;
            poco.DoWork();
            Assert.IsTrue(raisedEvent);
        }
    }
}