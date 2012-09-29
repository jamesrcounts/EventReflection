namespace EventReflection.Demo
{
    using System;
    using System.Linq;
    using System.Windows.Forms;
    using ApprovalTests.WinForms;
    using ApprovalUtilities.Utilities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class WinFormsDemo
    {
        [TestMethod]
        public void AsEnumerableMethodAdaptsEventHandlerList()
        {
            var button = GetTestButton();
            ApprovalTests.Approvals.VerifyAll(
                button.GetEventHandlerList().AsEnumerable(),
                e => e.WritePropertiesToString());
        }

        [TestMethod]
        public void ButtonHasNoHead()
        {
            Assert.IsNull(new Button().GetEventHandlerList().GetHead());
        }

        [TestMethod]
        public void DemoFormEventHandlerListHasHead()
        {
            var eventHandlerList = new DemoForm().GetEventHandlerList();
            Assert.AreEqual("ListEntry", eventHandlerList.GetHead().GetType().Name);
        }

        [TestMethod]
        public void GetEventHandlerList()
        {
            Assert.IsNotNull(new DemoForm().GetEventHandlerList());
        }

        [TestMethod]
        public void GetEventsForDemoFormEventHandlers()
        {
            ApprovalTests.Approvals.VerifyAll(
                new DemoForm().GetEventsForTypes(typeof(EventHandler)),
                string.Empty);
        }

        [TestMethod]
        public void GetEventTypeForDemoForm()
        {
            ApprovalTests.Approvals.VerifyAll(
                ReflectionUtility.GetEventTypes(new DemoForm()), string.Empty);
        }

        [TestMethod]
        public void ListEntryIsWrapped()
        {
            var listEntryWrapper = new ListEntryWrapper(GetListEntry());
            ApprovalTests.Approvals.Verify(listEntryWrapper.WritePropertiesToString());
        }

        [TestMethod]
        public void ListHasMoreThanOneEntry()
        {
            var button = GetTestButton();
            var wrapper = new ListEntryWrapper(button.GetEventHandlerList().GetHead());
            ApprovalTests.Approvals.Verify(wrapper.WritePropertiesToString());
        }

        [TestMethod]
        public void NullHasNoEventHandlerList()
        {
            Assert.IsNull(ReflectionUtility.GetEventHandlerList(null));
        }

        [TestMethod]
        public void NullHasNoHead()
        {
            Assert.IsNull(ReflectionUtility.GetHead(null));
        }

        [TestMethod]
        public void NullListIsEmpty()
        {
            var button = new Button();
            Assert.IsFalse(button.GetEventHandlerList().AsEnumerable().Any());
        }

        [TestMethod]
        public void PocoHasNoEventHandlerList()
        {
            Assert.IsNull(new Poco().GetEventHandlerList());
        }

        [TestMethod]
        public void RetrieveListEntryWithReflection()
        {
            var head = new DemoForm().GetEventHandlerList().GetHead();
            ApprovalTests.Approvals.Verify(head.GetType().FullName);
        }

        [TestMethod]
        public void SpoilerHasNoEventHandlerList()
        {
            Assert.IsNull(new Spoiler().GetEventHandlerList());
        }

        [TestMethod]
        public void VerifyDemoFormEvents()
        {
            EventUtility.VerifyEventCallbacks(new DemoForm());
        }

        [TestMethod]
        public void VerifyDemoFormView()
        {
            WinFormsApprovals.Verify(new DemoForm());
        }

        [TestMethod]
        public void WrongObjectIsntWrapped()
        {
            var listEntryWrapper = new ListEntryWrapper(new object());
            ApprovalTests.Approvals.Verify(listEntryWrapper.WritePropertiesToString());
        }

        private static Button GetTestButton()
        {
            var button = new Button();
            button.Click += (s, e) => { return; };
            button.LostFocus += (s, e) => { return; };
            return button;
        }

        private object GetListEntry()
        {
            return new DemoForm().GetEventHandlerList().GetHead();
        }
    }
}