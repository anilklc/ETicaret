using ETicaret.Domain.Entities;
using ETicaret.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Persistence.Contexts
{
    public class ETicaretDbContext : DbContext
    {
        public ETicaretDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        //savechanges neden ovveride ettik burda sebebi şu her defasında updated ve created işleminden sonra
        //datetime kendimiz eklemektense ınterceptor ile işlem sırasında araya girerek datetime ı verip uğraşmak zorunda kalmayız
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //changetracker entityler üzerinde yapılan değişiklikleri yada yeni eklenen verilerin yakalanmasını sağlayan propertydir.
            var datas= ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas) 
            {
                var _ = data.State switch 
                { 
                    EntityState.Added=>data.Entity.CreatedDate=DateTime.Now,
                    EntityState.Modified=>data.Entity.UpdatedDate=DateTime.Now,      
                    _=>DateTime.Now
                };
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
