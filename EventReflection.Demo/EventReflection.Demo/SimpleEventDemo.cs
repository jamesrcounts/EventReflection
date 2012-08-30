using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventReflection.Demo
{
    [TestClass]
    public class SimpleEventDemo
    {
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