namespace EventReflection.Demo
{
    using System.Text;

    using ApprovalTests;

    using ApprovalUtilities.Utilities;

    public static class EventUtility
    {
        public static void VerifyEventCallbacks(object value)
        {
            StringBuilder buffer = null;
            if (value != null)
            {
                buffer = new StringBuilder();

                buffer.AppendLine("Event callbacks for {0}".FormatWith(value.GetType().Name)).AppendLine();
                foreach (var callback in value.GetEventCallbacks())
                {
                    buffer.AppendLine(callback.ToString());
                }
            }

            Approvals.Verify(buffer);
        }
    }
}