using Play.Inventory.Service.Dtos;
using Play.Inventory.Service.Utilities;

namespace Play.Inventory.Service
{
    public class ExternalCatalogService : IExternalCatalogService
    {
        private readonly HttpClient _httpClient;
        public ExternalCatalogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CatalogItemDto>> GetCatalogItems()
        {
            IEnumerable<CatalogItemDto> catalogItems;
            try
            {
                var httpResponse = await _httpClient.GetAsync($"/items");
                if (httpResponse.IsSuccessStatusCode)
                {
                    var stream = await httpResponse.Content.ReadAsStreamAsync();
                    catalogItems = JsonParseContent.DeserializeJsonFromStream<IEnumerable<CatalogItemDto>>(stream);
                }
                else
                {
                    catalogItems = new List<CatalogItemDto>();
                }

                return catalogItems;
            }
            catch (System.Exception ex)
            {
                // TODO
                throw new Exception();
            }
        }
    }

}