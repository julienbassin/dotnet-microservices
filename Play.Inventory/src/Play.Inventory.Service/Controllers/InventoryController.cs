using Microsoft.AspNetCore.Mvc;
using Play.Common;
using Play.Inventory.Service.Dtos;
using Play.Inventory.Service.Entities.Models;

namespace Play.Inventory.Service
{
    [ApiController]
    [Route("Items")]
    public class InventoryController : ControllerBase
    {
        private readonly IRepository<InventoryItem> inventoryRepository;
        private readonly IExternalCatalogService catalogService;
        public InventoryController(IRepository<InventoryItem> inventoryRepository,
                                    IExternalCatalogService catalogService)
        {
            this.inventoryRepository = inventoryRepository;
            this.catalogService = catalogService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryItemDto>>> GetAsync(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return BadRequest();
            }

            var catalogItems = await catalogService.GetCatalogItems();
            var inventoryEntities = await inventoryRepository.GetAllAsync(item => item.UserId == userId);

            var InventoryItemDtos = inventoryEntities.Select(inventoryItem =>
           {
               var catalogItem = catalogItems.Single(catalogItem => catalogItem.Id == inventoryItem.CatalogItemId);
               return inventoryItem.AsDto(catalogItem.Name, catalogItem.Description);
           });
            return Ok(InventoryItemDtos);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(grantItemsDTO item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            try
            {
                // find item using Id
                var inventoryItem = await inventoryRepository.GetAsync(i => i.UserId == item.userId && i.CatalogItemId == item.catalogItemId);
                if (inventoryItem == null)
                {
                    var newItem = new InventoryItem
                    {
                        UserId = item.userId,
                        CatalogItemId = item.catalogItemId,
                        Quantity = item.Quantity,
                        AcquiredDate = DateTimeOffset.UtcNow
                    };

                    await inventoryRepository.CreateAsync(newItem);
                }
                else
                {
                    inventoryItem.Quantity += item.Quantity;
                }

            }
            catch (System.Exception ex)
            {
                // TODO
                throw new ArgumentException();
            }

            return NoContent();
        }
    }
}