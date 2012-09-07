namespace EventReflection.Demo
{
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ComplexEventDemo
    {
        [TestMethod]
        public void PocoExtensionTest()
        {
            var target = new PocoExtension();
            target.ProcessStarted += Domain.HandleProcessStarted;
            target.PropertyChanged += Domain.HandlePropertyChanged;
            EventUtility.VerifyEventCallbacks(target);
        }

        [TestMethod]
        public void NullHasNoEventCallbacks()
        {
            Assert.IsFalse(ReflectionUtility.GetEventCallbacks(null).Any());
        }
    }
}