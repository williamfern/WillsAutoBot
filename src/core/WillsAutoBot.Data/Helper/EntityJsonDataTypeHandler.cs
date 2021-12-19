using Microsoft.Azure.Cosmos.Table;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace WillsAutoBot.Data.Helper
{
    /// <summary>
    /// The Handler takes care of member properties decorated with EntityJsonDataType attribute
    /// when reading and writing to table storage.
    /// </summary>
    internal class EntityJsonDataTypeHandler
    {
        public static void Serialize<TEntity>(TEntity entity, IDictionary<string, EntityProperty> results)
        {
            entity.GetType()
                .GetProperties()
                .Where(x => x.GetCustomAttributes(typeof(EntityJsonDataTypeAttribute), false).Count() > 0)
                .ToList()
                .ForEach(x => results.Add(x.Name, new EntityProperty(
                    JsonConvert.SerializeObject(x.GetValue(entity)))));
        }

        public static void Deserialize<TEntity>(TEntity entity, IDictionary<string, EntityProperty> properties)
        {
            entity.GetType()
                .GetProperties()
                .Where(x => x.GetCustomAttributes(typeof(EntityJsonDataTypeAttribute), false).Count() > 0)
                .ToList()
                .ForEach(x => x.SetValue(entity,
                    properties.TryGetValue(x.Name, out var property)
                        ? JsonConvert.DeserializeObject(property.StringValue, x.PropertyType)
                        : null));
        }
    }
}