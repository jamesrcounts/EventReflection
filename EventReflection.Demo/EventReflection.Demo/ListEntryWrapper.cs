using System;

namespace EventReflection.Demo
{
    using System.ComponentModel;
    using System.Reflection;

    public class ListEntryWrapper
    {
        private const BindingFlags NonPublicInstance = BindingFlags.NonPublic | BindingFlags.Instance;

        private static readonly Lazy<FieldInfo> HandlerInfo =
            new Lazy<FieldInfo>(() => ListEntryType.Value.GetField("handler", NonPublicInstance));

        private static readonly Lazy<FieldInfo> KeyInfo =
            new Lazy<FieldInfo>(
                () => ListEntryType.Value.GetField("key", NonPublicInstance));

        private static readonly Lazy<Type> ListEntryType =
            new Lazy<Type>(() => typeof(EventHandlerList).GetNestedType("ListEntry", BindingFlags.NonPublic));

        private static readonly Lazy<FieldInfo> NextInfo =
            new Lazy<FieldInfo>(() => ListEntryType.Value.GetField("next", NonPublicInstance));

        private readonly object listEntry;

        public ListEntryWrapper(object value)
        {
            if (ReflectionUtility.GetType(value) == ListEntryType.Value)
            {
                this.listEntry = value;
            }
        }

        public Delegate Handler
        {
            get
            {
                return this.listEntry == null ?
                    null :
                    HandlerInfo.Value.GetValue<Delegate>(this.listEntry);
            }
        }

        public object Key
        {
            get
            {
                return this.listEntry == null ?
                    null :
                    KeyInfo.Value.GetValue(this.listEntry);
            }
        }

        public ListEntryWrapper Next
        {
            get
            {
                if (this.listEntry == null)
                {
                    return null;
                }

                object nextValue = NextInfo.Value.GetValue(this.listEntry);
                return nextValue == null ? null : new ListEntryWrapper(nextValue);
            }
        }
    }
}