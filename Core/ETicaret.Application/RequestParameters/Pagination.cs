using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Application.RequestParameters
{
    //burda sayfalama işlemi yaptık
    public record Pagination
    { 
        // 0ve 5 i neden girdik varsayılan sınırlarımız bunlar olsun diye
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}
