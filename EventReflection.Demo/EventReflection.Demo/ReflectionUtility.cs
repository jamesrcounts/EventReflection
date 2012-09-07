namespace EventReflection.Demo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class ReflectionUtility
    {
        public const BindingFlags NonPublicInstance = BindingFlags.Instance | BindingFlags.NonPublic;

        public static IEnumerable<FieldInfo> EnumerateFieldsWithInherited(
            this Type typeInfo,
            BindingFlags bindingFlags)
        {
            for (var type = typeInfo; type != null; type = type.BaseType)
            {
                foreach (var fieldInfo in type.GetFields(bindingFlags))
                {
                    yield return fieldInfo;
                }
            }
        }

        public static IEnumerable<EventCallback> GetEventHandlers(this object value)
        {
            if (value == null)
            {
                return null;
            }

            return from fieldInfo in value.GetType().EnumerateFieldsWithInherited(NonPublicInstance)
                   where typeof(EventHandler).IsAssignableFrom(fieldInfo.FieldType)
                   let callback = fieldInfo.GetValue<EventHandler>(value)
                   where callback != null
                   select new EventCallback(fieldInfo.Name, callback);
        }

        public static T GetValue<T>(this FieldInfo fi, object value)
        {
            return (T)fi.GetValue(value);
        }
    }
}