using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Persistence
{
    static class Configuration
    {
        //neden Configuration diye ayrı class içine koyup direkt Dbcontexte bırakmadık çünkü başka bir
        //alandada kullanmamız gerekirse tekrar tekrar aynı yapıyı yazmaktansa direkt burdan çekmek daha pratik olur
        static public string ConfiguraionString
        {
            get
            {
                ConfigurationManager configurationManager = new();
                //eklediğimiz configuration.json paketiyle birlikte geliyor aşşağıdaki iki fonksiyon
                //birisi json dosyasının nerde olduğunu belirtmek için diğeri json dosyasının ismini
                //belirtmek için ve onun okunması için kullandık
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/ETicaret.API"));
                configurationManager.AddJsonFile("appsettings.json");

                return configurationManager.GetConnectionString("sqlConnection");
            }
        }
    }
}
