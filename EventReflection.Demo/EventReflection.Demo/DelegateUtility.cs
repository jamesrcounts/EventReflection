namespace EventReflection.Demo
{
    using System;
    using System.Linq;

    using ApprovalTests;

    public static class DelegateUtility
    {
        public static void VerifyInvocationList(Delegate value)
        {
            Approvals.VerifyAll(
                value.GetInvocationList().Select(d => d.Method),
                string.Empty);
        }
    }
}