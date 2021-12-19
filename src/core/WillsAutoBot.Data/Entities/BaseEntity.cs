using Microsoft.Azure.Cosmos.Table;
using System.Collections.Generic;
using WillsAutoBot.Data.Helper;

namespace WillsAutoBot.Data.Entities
{
    /// <summary>
    /// Represents a Cloud Sight entity.
    /// </summary>
    public abstract class BaseEntity : TableEntity
    {
        /// <summary>
        /// Gets the partition key for the entity based on the entity-specific naming rules.
        /// </summary>
        /// <returns>A string representing a partition key.</returns>
        protected abstract string GetPartitionKey();

        /// <summary>
        /// Sets the partition and row keys for the entity based on the entity-specific naming rules.
        /// </summary>
        protected abstract void SetPartitionAndRowKeys();

        /// <inheritdoc />
        public override void ReadEntity(IDictionary<string, EntityProperty> properties,
            OperationContext operationContext)
        {
            base.ReadEntity(properties, operationContext);

            // Handles JSON and Enum Properties 
            EntityJsonDataTypeHandler.Deserialize(this, properties);
            EntityEnumDataTypeHandler.Deserialize(this, properties);
        }

        /// <inheritdoc />
        public override IDictionary<string, EntityProperty> WriteEntity(OperationContext operationContext)
        {
            var serializedProperties = base.WriteEntity(operationContext);

            // Handles JSON and Enum Properties on Write.
            EntityJsonDataTypeHandler.Serialize(this, serializedProperties);
            EntityEnumDataTypeHandler.Serialize(this, serializedProperties);
            return serializedProperties;
        }
    }
}