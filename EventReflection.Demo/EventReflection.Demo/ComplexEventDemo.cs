using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventReflection.Demo
{
    using ApprovalTests;

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
    }
}