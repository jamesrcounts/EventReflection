namespace EventReflection.Demo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using ApprovalTests.Reporters;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [UseReporter(typeof(DiffReporter))]
    public class DelegateDemo
    {
        [TestMethod]
        public void GetInvocationList()
        {
            Func<bool> truth = Domain.AlwaysTrue;
            DelegateUtility.VerifyInvocationList(truth);
        }

        [TestMethod]
        public void GetMultiInvocationList()
        {
            Func<bool> truth = Domain.AlwaysTrue;
            Func<bool> truthy = Domain.NeverFalse;
            DelegateUtility.VerifyInvocationList(Delegate.Combine(truth, truthy));
        }

        [TestMethod]
        public void UnicastDelegateHasInvocationList()
        {
            Func<bool> truth = Domain.AlwaysTrue;
            Assert.AreEqual(
                truth.Method,
                truth.GetInvocationList().Select(i => i.Method).Single());
        }

        [TestMethod]
        public void MulticastDelegateInvocationListContainsMethod()
        {
            Func<bool> truth = Domain.AlwaysTrue;
            Func<bool> truthy = Domain.NeverFalse;
            Delegate combined = Delegate.Combine(truth, truthy);
            Assert.AreEqual(
                combined.Method,
                combined.GetInvocationList().Select(i => i.Method).Last());
        }

        [TestMethod]
        public void CombinedDelegatesAreFlattened()
        {
            Func<bool> truth = Domain.AlwaysTrue;
            Func<bool> truthy = Domain.NeverFalse;
            Func<bool> answer43 = Domain.AlwaysTrue;
            Delegate combined = Delegate.Combine(truthy, answer43);
            Delegate flattened = Delegate.Combine(truth, combined);
            DelegateUtility.VerifyInvocationList(flattened);
        }

        public class Domain
        {
            public static bool AlwaysTrue()
            {
                return true;
            }

            public static bool NeverFalse()
            {
                return true;
            }
        }
    }
}