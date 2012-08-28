namespace EventReflection.Demo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using ApprovalTests;
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
            IEnumerable<MethodInfo> methods = truth.GetInvocationList().Select(d => d.Method);
            Approvals.VerifyAll(methods, string.Empty);
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