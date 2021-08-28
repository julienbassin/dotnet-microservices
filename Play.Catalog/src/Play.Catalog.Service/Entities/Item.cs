using System;

namespace Play.Catalog.Service.Entites
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }

    public class Item : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}