using ETicaret.Domain.Entities.Common;
using System.Linq.Expressions;

namespace ETicaret.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {  
        //tracking ile üzerinde işlem yapmadığımız ve sadece listeleyeceğimiz durumlarda
        //ekstradan sistemimize yük bindirmemek için tracking false yaparak controllerde
        //sistemimize gerksiz yük bindirmeyerek ilerleyebiliriz
        IQueryable<T> GetAll(bool tracking=true);

        //veritabanındak where şartı gibi kullanıcağız
        IQueryable<T> GetWhere(Expression<Func<T,bool>> method, bool tracking = true);

        //burda ise şarta bağlı olan tek veriyi getircek
        Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking= true);

        //burda ise verilen id ye göre
        Task<T> GetByIdAsync(int id, bool tracking = true);
    }

}
