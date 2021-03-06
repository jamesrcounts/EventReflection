﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventReflection.Demo
{
    using System.Linq;

    [TestClass]
    public class SimpleEventDemo
    {
        [TestMethod]
        public void GetPocoEventInvocationList()
        {
            var poco = new Poco();
            poco.ProcessCompleted += Domain.HandleProcessCompleted;
            EventUtility.VerifyEventCallbacks(poco);
        }

        [TestMethod]
        public void MulticastEvent()
        {
            var poco = new Poco();
            poco.ProcessCompleted += Domain.HandleProcessCompleted;
            poco.ProcessCompleted += Domain.HandleProcessStarted;
            EventUtility.VerifyEventCallbacks(poco);
        }

        [TestMethod]
        public void MultipleEvents()
        {
            var poco = new Poco();
            poco.ProcessCompleted += Domain.HandleProcessCompleted;
            poco.ProcessStarted += Domain.HandleProcessStarted;
            EventUtility.VerifyEventCallbacks(poco);
        }

        [TestMethod]
        public void NullHasNoProcessCompletedHandler()
        {
            Assert.IsFalse(ReflectionUtility.GetEventHandlers(null).Any());
        }

        [TestMethod]
        public void PocoClientListensToPoco()
        {
            Poco poco = new Poco();
            PocoClient client = new PocoClient(poco);
            EventUtility.VerifyEventCallbacks(poco);
        }

        [TestMethod]
        public void NullHasNoEvents()
        {
            EventUtility.VerifyEventCallbacks(null);
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