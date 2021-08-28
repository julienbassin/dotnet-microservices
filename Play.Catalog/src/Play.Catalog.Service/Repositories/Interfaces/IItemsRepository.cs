using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Play.Catalog.Service.Entites;

namespace Play.Catalog.Service.Repositories.Interfaces
{
    public interface IItemsRepository
    {
        Task CreateAsync(Item entity);
        Task<IReadOnlyCollection<Item>> GetAllAsync();
        Task<Item> GetAsync(Guid Id);
        Task RemoveAsync(Guid Id);
        Task UpdateAsync(Item entity);
    }

}