using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Play.Catalog.Service.Entites;

namespace Play.Catalog.Service.Repositories.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        Task CreateAsync(T entity);
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task<T> GetAsync(Guid Id);
        Task RemoveAsync(Guid Id);
        Task UpdateAsync(T entity);
    }

}