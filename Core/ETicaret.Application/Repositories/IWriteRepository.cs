using ETicaret.Domain.Entities.Common;

namespace ETicaret.Application.Repositories
{
    public interface IWriteRepository <T> :IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T model);
        Task<bool> AddRangeAsync(List<T> data);
        Task<bool> RemoveAsync(int id);
        bool Remove(T model);
        bool RemoveRange(List<T> datas);
        bool Update(T model);
        Task<int> SaveAsync();
    }
}
