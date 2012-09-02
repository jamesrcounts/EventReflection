using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventReflection.Demo
{
    [TestClass]
    public class SimpleEventDemo
    {
        [TestMethod]
        public void GetPocoEventInvocationList()
        {
            var poco = new Poco();
            poco.ProcessCompleted += Domain.HandleProcessCompleted;
            EventUtility.VerifyEventCallbacks(poco.GetEventHandlers());
        }

        [TestMethod]
        public void MulticastEvent()
        {
            var poco = new Poco();
            poco.ProcessCompleted += Domain.HandleProcessCompleted;
            poco.ProcessCompleted += Domain.HandleProcessStarted;
            EventUtility.VerifyEventCallbacks(poco.GetEventHandlers());
        }

        [TestMethod]
        public void MultipleEvents()
        {
            var poco = new Poco();
            poco.ProcessCompleted += Domain.HandleProcessCompleted;
            poco.ProcessStarted += Domain.HandleProcessStarted;
            EventUtility.VerifyEventCallbacks(poco.GetEventHandlers());
        }

        [TestMethod]
        public void NullHasNoProcessCompletedHandler()
        {
            Assert.IsNull(ReflectionUtility.GetEventHandlers(null));
        }

        [TestMethod]
        public void PocoClientListensToPoco()
        {
            Poco poco = new Poco();
            PocoClient client = new PocoClient(poco);
            EventUtility.VerifyEventCallbacks(poco.GetEventHandlers());
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