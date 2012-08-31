namespace EventReflection.Demo
{
    using System;
    using System.Linq;
    using System.Reflection;

    public static class ReflectionUtility
    {
        public const BindingFlags NonPublicInstance = BindingFlags.Instance | BindingFlags.NonPublic;

        public static EventHandler GetProcessCompletedHandler(this Poco poco)
        {
            var matchingFields = from fieldInfo in poco.GetType().GetFields(NonPublicInstance)
                                 where typeof(EventHandler).IsAssignableFrom(fieldInfo.FieldType)
                                     && fieldInfo.Name == "ProcessCompleted"
                                 select (EventHandler)fieldInfo.GetValue(poco);
            return matchingFields.Single();
        }
    }
}