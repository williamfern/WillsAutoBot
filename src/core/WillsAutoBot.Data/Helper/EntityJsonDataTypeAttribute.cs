using System;

namespace WillsAutoBot.Data.Helper
{
    /// <summary>
    /// Attribute Flag for Complex Data Objects which is to be stored as JSON string
    /// and needs to be retrieved as class objects.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    internal class EntityJsonDataTypeAttribute: Attribute
    {
        public EntityJsonDataTypeAttribute()
        {
        }
    }
}
