using System;
using Play.Common;

namespace Play.Inventory.Service.Entities.Models
{
    public class InventoryItem : IEntity
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public Guid UserId { get; set; }
        public Guid CatalogItemId { get; set; }

        public DateTimeOffset AcquiredDate { get; set; }



    }
}