using System;

namespace WillsAutoBot.Data.Helper
{
    /// <summary>
    /// Attribute Flag for Enum Data members which is to be stored as string
    /// and needs to be retrieved as enum value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    internal class EntityEnumDataTypeAttribute : Attribute
    {
        public EntityEnumDataTypeAttribute()
        {
        }
    }
}
