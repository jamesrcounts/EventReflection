namespace EventReflection.Demo
{
    using System.Collections.Generic;

    using ApprovalTests;

    public static class EventUtility
    {
        public static void VerifyEventCallbacks(IEnumerable<EventCallback> callbacks)
        {
            Approvals.VerifyAll(callbacks, c => c.ToString());
        }
    }
}