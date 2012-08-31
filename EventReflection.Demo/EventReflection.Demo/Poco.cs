namespace EventReflection.Demo
{
    using System;

    public class Poco
    {
        public event EventHandler ProcessCompleted;

        public int DoWork()
        {
            int result = 0;

            // ...Do very hard work...
            OnProcessCompleted(this, EventArgs.Empty);
            return result;
        }

        public EventHandler GetProcessCompletedHandler()
        {
            return this.ProcessCompleted;
        }

        protected virtual void OnProcessCompleted(object sender, EventArgs e)
        {
            EventHandler handler = ProcessCompleted;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
    }
}