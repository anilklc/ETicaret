using ETicaret.Application.Repositories;
using ETicaret.Domain.Entities.Common;
using ETicaret.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly ETicaretDbContext _context;

        public ReadRepository(ETicaretDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll(bool tracking = true)
        {
            //eğer Track işlemi yapılmıycaksa if e girip track işlemini kapatyoruz
            var query = Table.AsQueryable();
            if (!tracking)
            {
                query=query.AsNoTracking();
            }
            return query;
        }

        public async Task<T> GetByIdAsync(int id, bool tracking = true)
        {
            //return await Table.FirstOrDefaultAsync(data=>data.Id == id);
            var query=Table.AsQueryable();
            if (!tracking)
            {
                query = Table.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(data=>data.Id==id);
        
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query= Table.AsQueryable();
            if (!tracking)
            {
                query = Table.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(method);
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query=Table.Where(method);
            if (!tracking)
            {
                query=query.AsNoTracking();
            }
            return query;
        }
    }
}
