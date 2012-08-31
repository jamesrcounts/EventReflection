namespace EventReflection.Demo
{
    using System;

    public class PocoClient
    {
        private readonly Poco primitive;

        public PocoClient(Poco primitive)
        {
            this.primitive = primitive;
            this.primitive.ProcessCompleted += this.LogCompletionTime;
        }

        private void LogCompletionTime(object sender, EventArgs e)
        {
            // write to log...
        }
    }
}