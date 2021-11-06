using Play.Inventory.Service.Dtos;

namespace Play.Inventory.Service
{
    public interface IExternalCatalogService
    {
        Task<IEnumerable<CatalogItemDto>> GetCatalogItems();
    }
}