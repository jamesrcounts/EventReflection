namespace EventReflection.Demo
{
    using System;
    using System.Text;

    using ApprovalUtilities.Utilities;

    public class EventCallback
    {
        public EventCallback(string name, Delegate callback)
        {
            this.Name = name;
            this.Callback = callback;
        }

        public Delegate Callback { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            var buffer = new StringBuilder();
            buffer.AppendLine(this.Name);
            var delegates = this.Callback.GetInvocationList();
            for (int index = 0; index < delegates.Length; index++)
            {
                buffer.AppendLine("\t[{0}] {1}".FormatWith(index, delegates[index].Method));
            }

            return buffer.ToString();
        }
    }
}