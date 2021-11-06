using System;

namespace Play.Inventory.Service.Dtos
{

    public record grantItemsDTO(Guid userId, Guid catalogItemId, int Quantity);
    public record InventoryItemDto(Guid catalogItemId, string name, string description, int Quantity, DateTimeOffset AcquiredDate);
    public record CatalogItemDto(Guid Id, string Name, string Description);

}