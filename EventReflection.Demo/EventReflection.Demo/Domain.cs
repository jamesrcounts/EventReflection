namespace EventReflection.Demo
{
    using System;
    using System.ComponentModel;

    public class Domain
    {
        public static bool AlwaysTrue()
        {
            return true;
        }

        public static void HandleProcessCompleted(object sender, EventArgs e)
        {
            return;
        }

        public static void HandleProcessStarted(object sender, EventArgs e)
        {
            return;
        }

        public static void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            return;
        }

        public static bool NeverFalse()
        {
            return true;
        }
    }
}