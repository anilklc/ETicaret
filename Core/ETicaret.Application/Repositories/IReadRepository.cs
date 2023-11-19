using ETicaret.Domain.Entities.Common;
using System.Linq.Expressions;

namespace ETicaret.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();

        //veritabanındak where şartı gibi kullanıcağız
        IQueryable<T> GetWhere(Expression<Func<T,bool>> method);

        //burda ise şarta bağlı olan tek veriyi getircek
        Task<T> GetSingleAsync(Expression<Func<T, bool>> method);

        //burda ise verilen id ye göre
        Task<T> GetByIdAsync(int id);
    }

}
