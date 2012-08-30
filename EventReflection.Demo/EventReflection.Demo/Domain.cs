namespace EventReflection.Demo
{
    using System;

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

        public static void HandleProcessCompleted(object sender, EventArgs e)
        {
            return;
        }
    }
}