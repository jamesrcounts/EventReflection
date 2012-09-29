namespace EventReflection.Demo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;

    public static class ReflectionUtility
    {
        public const BindingFlags NonPublicInstance = BindingFlags.Instance | BindingFlags.NonPublic;

        public static IEnumerable<ListEntryWrapper> AsEnumerable(this EventHandlerList source)
        {
            object value = source.GetHead();
            if (value == null)
            {
                yield break;
            }

            for (var head = new ListEntryWrapper(value); head != null; head = head.Next)
            {
                yield return head;
            }
        }

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
            return value.GetEventsForTypes(GetEventTypes(value).ToArray());
        }

        public static EventHandlerList GetEventHandlerList(this object value)
        {
            var lists = from fieldInfo in GetType(value).EnumerateFieldsWithInherited(NonPublicInstance)
                        where
                            fieldInfo.Name == "events" &&
                            typeof(EventHandlerList).IsAssignableFrom(fieldInfo.FieldType)
                        select fieldInfo.GetValue<EventHandlerList>(value);

            return lists.SingleOrDefault();
        }

        public static IEnumerable<EventCallback> GetEventHandlers(this object value)
        {
            return value.GetEventsForTypes(typeof(EventHandler));
        }

        public static IEnumerable<EventCallback> GetEventsForTypes(
            this object value,
            params Type[] types)
        {
            return from fieldInfo in GetType(value).EnumerateFieldsWithInherited(NonPublicInstance)
                   where types.Any(t => t == fieldInfo.FieldType)
                   let callback = fieldInfo.GetValue<Delegate>(value)
                   where callback != null
                   select new EventCallback(fieldInfo.Name, callback);
        }

        public static IEnumerable<Type> GetEventTypes(object value)
        {
            return GetType(value).GetEvents().Select(ei => ei.EventHandlerType).Distinct();
        }

        public static object GetHead(this EventHandlerList value)
        {
            var headInfo = GetType(value).GetField("head", NonPublicInstance);
            return headInfo == null ? null : headInfo.GetValue(value);
        }

        public static Type GetType(object value)
        {
            return value == null ? typeof(void) : value.GetType();
        }

        public static T GetValue<T>(this FieldInfo fi, object value)
        {
            return (T)fi.GetValue(value);
        }
    }
}