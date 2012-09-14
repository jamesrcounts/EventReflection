namespace EventReflection.Demo
{
    using System;

    using ApprovalTests.WinForms;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class WinFormsDemo
    {
        [TestMethod]
        public void VerifyDemoFormView()
        {
            WinFormsApprovals.Verify(new DemoForm());
        }

        [TestMethod]
        public void GetEventTypeForDemoForm()
        {
            ApprovalTests.Approvals.VerifyAll(
                ReflectionUtility.GetEventTypes(new DemoForm()), string.Empty);
        }

        [TestMethod]
        public void GetEventsForDemoFormEventHandlers()
        {
            ApprovalTests.Approvals.VerifyAll(
                new DemoForm().GetEventsForTypes(typeof(EventHandler)),
                string.Empty);
        }

        [TestMethod]
        public void VerifyDemoFormEvents()
        {
            EventUtility.VerifyEventCallbacks(new DemoForm());
        }
    }
}