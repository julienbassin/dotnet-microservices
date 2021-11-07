namespace Play.Catalog.Service
{
    public static class Extensions
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto(Item.Id, item.Name, item.Description, item.Price, item.CreatedDate);
        }
    }
}