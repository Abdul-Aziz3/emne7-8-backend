using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//    ID,Name,CountryCode,District,Population
//    1,Kabul,AFG,Kabol,1780000                   


namespace WorldModelLibrary.models
{
    //public record City(int Id, string Name, string CountryCode, string District, int Population);

    
    public sealed class City
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public int Population { get; set; }
        public override string ToString()
        {
            return $"Id:{Id}, Name:{Name}, CountryCode:{CountryCode}, " +
                $"District:{District}, Population:{Population}";
        }
    }
}
