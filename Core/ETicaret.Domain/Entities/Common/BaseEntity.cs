using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Domain.Entities.Common
{
    public class BaseEntity
    {
        //Zaten tüm enttiylerimde olucaklar tekrar tekrar yazmaktansa baseEntityde tutmak daha mantıklı
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set;}
    }
}
