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

        public static IEnumerable<EventCallback> GetEventCallbacks(
            this object value)
        {
            var types = value.GetType().GetEvents()
                .Select(ei => ei.EventHandlerType).Distinct();
            return value.GetEventsForTypes(types.ToArray());
        }

        public static IEnumerable<EventCallback> GetEventHandlers(this object value)
        {
            return value.GetEventsForTypes(typeof(EventHandler));
        }

        public static IEnumerable<EventCallback> GetEventsForTypes(
            this object value,
            params Type[] types)
        {
            if (value == null)
            {
                return null;
            }

            return from fieldInfo in value.GetType().EnumerateFieldsWithInherited(NonPublicInstance)
                   where types.Any(t => t == fieldInfo.FieldType)
                   let callback = fieldInfo.GetValue<Delegate>(value)
                   where callback != null
                   select new EventCallback(fieldInfo.Name, callback);
        }

        public static T GetValue<T>(this FieldInfo fi, object value)
        {
            return (T)fi.GetValue(value);
        }
    }
}