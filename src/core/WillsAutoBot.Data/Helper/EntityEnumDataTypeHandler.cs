using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using WillsAutoBot.Data.Helper;
using WillsAutoBot.Utilities.Extensions;

namespace WillsAutoBot.Data.Helper
{
    /// <summary>
    /// The Handler takes care of member properties decorated with EntityEnumDataType attribute
    /// when reading and writing to table storage.
    /// </summary>
    internal class EntityEnumDataTypeHandler
    {
        public static void Serialize<TEntity>(TEntity entity, IDictionary<string, EntityProperty> results)
        {
            entity.GetType()
                .GetProperties()
                .Where(x => x.GetCustomAttributes(typeof(EntityEnumDataTypeAttribute), false).Any())
                .ToList()
                .ForEach(x => results.Add(x.Name, new EntityProperty(x.GetValue(entity) != null ? x.GetValue(entity).ToString() : null)));
        }

        public static void Deserialize<TEntity>(TEntity entity, IDictionary<string, EntityProperty> properties)
        {
            entity.GetType()
                .GetProperties()
                .Where(x => x.GetCustomAttributes(typeof(EntityEnumDataTypeAttribute), false).Any())
                .ToList()
                .ForEach(x =>
                {
                    if (properties.ContainsKey(x.Name) && !properties[x.Name].StringValue.IsNullOrWhiteSpace())
                        x.SetValue(entity, Enum.Parse(x.PropertyType, properties[x.Name].StringValue));
                });
        }
    }
}
